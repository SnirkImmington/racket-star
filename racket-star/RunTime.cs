using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace RacketStar
{
    /// <summary>
    /// Containing methods for compiling code and maintaning a stack.
    /// </summary>
    static class RunTime
    {
        /// <summary>
        /// BindingFlags used for searching for members.
        /// </summary>
        private const BindingFlags SEARCH_FLAGS =
            BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.IgnoreReturn | BindingFlags.Static | BindingFlags.Instance;

        /// <summary>
        /// MemberTypes used for searching for members.
        /// </summary>
        private const MemberTypes MEMBER_TYPES =
            MemberTypes.Field | MemberTypes.Method | MemberTypes.Property;

        public static List<Assembly> ReferencedAssemblies = new List<Assembly>();

        public static Dictionary<Assembly, List<String>> AssembliesAndNamespaces = new Dictionary<Assembly, List<string>>();

        /// <summary>
        /// The stack. A new scope is added every time a function or variable is created.
        /// </summary>
        public static LinkedList<Scope> Scopes = new LinkedList<Scope>();

        /// <summary>
        /// A cache of C# methods which we may or may not use.
        /// A compiled racket-sharp program may
        /// </summary>
        public static Dictionary<string, object> NativeMethods;

        public static LinkedListNode<Scope> Current;

        /// <summary>
        /// Basic types used by racket
        /// </summary>
        public static Type[] BASIC_TYPES = new Type[] 
        { 
            typeof(string), typeof(bool), typeof(int), typeof(char), 
            typeof(float), typeof(double), typeof(object),
            typeof(LinkedList<dynamic>), typeof(LinkedList<object>)
        };

        public static void AddBasicAssemblies()
        {
            ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(string))); // System

            AssembliesAndNamespaces.Add(ReferencedAssemblies[0], new List<string>(
                new string[] { "System", "System.Collections.Generic" }));
        }

        /// <summary>
        /// Initializes the runtime values.
        /// </summary>
        // TODO make this an instance class, add constructor, use instance.
        public static void Initialize()
        {
            Scopes = new LinkedList<Scope>(); 
            Scopes.AddFirst(new Scope());
            Current = Scopes.Last;
        }

        /// <summary>
        /// Searches the scope for name, returning the first result (local->global)
        /// </summary>
        public static object SearchScope(string name)
        {
            var currNode = Current;
            // Stop when we can't go backwards through the node anymore.
            while (currNode != null)
            {
                // Try to get the current node's variable
                var variable = currNode.Value.GetVariableValue(name);

                // If we can't, we need to go deeper.
                if (variable == null)
                    currNode = currNode.Previous;

                // If we got it, return it.
                else return variable;
            }
            return null;
        }

        /// <summary>
        /// Searches the stack and C# reflection for a method or property to invoke.
        /// </summary>
        /// <param name="name">Name of method/field</param>
        /// <param name="arguments">Arguments to method/object to access.</param>
        /// <returns></returns>
        public static object GetVariableValue(string name, object[] arguments, Type[] typeArguments)
        {
            #region TODO search the local stack for the variables

            // Search the scope for the variable
            // Might also want to do a type check, or add a param to SearchScope to specify method/variable
            var variable = SearchScope(name);
            if (variable != null)
                return variable;

            #endregion

            #region Get Name Thing

            // At this point, we need to search C# libs.
            // rakcet -> C# naming:
            // string-format -> String.Format
            // string_builder-new -> new StringBuilder (LAAATER)
            // (string_builder-length builder) -> StringBuilder.Length
            var names = Utils.GetCSharpStrings(name);

            // Shortcut for "[type]-method".
            // (.length "hello world") -> (string-length "hello world")
            // Remember to prevent creating methods with names starting with a dot.
            if (name[0] == '.')
            {
                if (arguments.Length == 0)
                    throw new InvalidOperationException("You can't call a . method with no arguments.");

                // Get the type needed
                var argType = arguments[0].GetType();

                // Search for member.
                var dotMembers = argType.GetMember(names.Last().Substring(1), SEARCH_FLAGS);

                return GetReflectionValue(arguments, typeArguments, dotMembers);

            }

            // If we can't parse the string properly for some reason at this point we're done.
            if (names.Length == 0) return null;

            // Get the name of the method.
            string methodName = names.Last();

            #endregion

            #region Get types

            Type type = null;

            //var searchName = (typeArguments.Length == 0) ? name : name + '`' + typeArguments[]

            // Loop through all but the last method.
            for (int i = 0; i < names.Length - 1; i++)
            {
                if (name[i] == '-') break; //Perhaps we should just convert to '_' and let there be collisions?
                //typeBuilder.Append(name[i]);

                // If the type doesn't exist, it's the first time, get the base type.
                if (type == null)
                {
                    // Search the type system for the type, using the System prefix.
                    // TODO add in searches for referenced assemblies.
                    foreach (var assem in ReferencedAssemblies)
                    {
                        type = assem.GetType(names[0], false, false);
                        // Try to get the type from the assembly, using a special assembly resolver for list`T` and such.
                        //type = Type.GetType(assemName + '.' + names[0], null, GetTypeFromAssembly, false, false);

                        if (type != null) break;
                    }
                    if (type == null) throw new TypeAccessException("Could not find type.");
                    continue;
                }
                else if (names.Length > 2)
                {
                    // First, try to get a nested type. Usually used for l
                    var subType = type.GetTypeInfo().GetNestedType(names[i], BindingFlags.IgnoreCase | BindingFlags.Public);
                    if (subType != null)
                    {
                        type = subType; continue;
                    }
                    else throw new TypeAccessException("Could not get nested type");
                }
            }

            // Create types to help the search.
            var types = new Type[arguments.Length];
            for (int i = 0; i < arguments.Length; i++)
                types[i] = arguments[i].GetType();

            #endregion

            #region Invoke methods

            // We have the types, do the new check now.
            if (methodName == "New")
            {
                // ArgumentNull and Argument Exceptions should be okay
                var constructor = type.GetConstructor(types);

                if (constructor == null) throw new MissingMemberException("No constructor found!");

                return constructor.Invoke(arguments.Skip(1).ToArray());
            }

            // Get the members.
            var members = type.GetMember(methodName, MEMBER_TYPES, SEARCH_FLAGS);

            // Use the subroutine to return the value.
            return GetReflectionValue(arguments, typeArguments, members);

            #endregion
        }

        /// <summary>
        /// Get the invocations of C# reflection info.
        /// </summary>
        /// <param name="arguments">Arguments to a method.</param>
        /// <param name="members">Members to search.</param>
        /// <returns>Reflectively invoked C# method/property/field.</returns>
        public static object GetReflectionValue(object[] arguments, Type[] typeArguments, MemberInfo[] members)
        {
            foreach (var member in members)
            {
                #region MemberInfo
                if (member is MethodInfo)
                {
                    // Get method info
                    var method = member as MethodInfo;
                    var parameters = method.GetParameters();
                    
                    // break 2 #thestruggle
                    bool isLegit = true;

                    // Handle the generics now
                    if (method.IsGenericMethod)
                    {
                        // Get the required generic arguments
                        var types = method.GetGenericArguments();

                        // TODO include possible type methods in an error thing.
                        if (types.Length != typeArguments.Length)
                        {
                            // Get the declaring type for analysis
                            var declaring = method.DeclaringType;
                            // TODO I have no idea if this happens/what to do about it. I don't think lambdas can be generic.
                            if (declaring == null) continue;

                            // If the type is generic try to use its generic parameters.
                            // Here's how Type.IsGenericType works:
                            // List<int> = isGenericType
                            // List<T> = isGenericTypeDefinition
                            if (declaring.IsGenericType) 
                            {
                                // Get its genericb arguments.
                                var genericParams = declaring.GetGenericArguments();

                                // For example, List<int>.Get<int> -> move the int over.
                                if (genericParams.Length == types.Length)
                                    method = method.MakeGenericMethod(genericParams);

                                // Expected 3 type arguments: TKey, TValue, TSomething, got 2: int, string.
                                else throw new InvalidTypeArgumentsException("");
                            }
                        }

                        // Convert the method if we can confirm the types then
                        else method = method.MakeGenericMethod(typeArguments);
                    }

                    // Different method types
                    if (method.IsStatic)
                    {
                        // Static methods must match parameters
                        if (parameters.Length != arguments.Length) continue;

                        // If there's no parameters let's just go ahead
                        if (parameters.Length == 0)
                            return method.Invoke(null, null);

                        // Check that each parameter is validly there.
                        for (int i = 0; i < parameters.Length; i++)
                            if (!arguments[i].GetType().IsAssignableFrom(parameters[i].ParameterType))
                            { isLegit = false; break; }

                        // break 2 #thestruggle
                        if (!isLegit) continue;

                        // Everything should be good.
                        return method.Invoke(null, arguments);
                    }
                    else // instance method
                    {
                        // Instance parameters start at 1.
                        if (parameters.Length != arguments.Length - 1) continue;

                        // Go ahead with no length params.
                        if (parameters.Length == 0)
                            return method.Invoke(arguments[0], null);

                        // Checkt the arguments, starting with one.
                        for (int i = 1; i < parameters.Length; i++)
                            if (!arguments[i].GetType().IsAssignableFrom(parameters[i].ParameterType))
                            { isLegit = false; break; }

                        // break 2 #tehstruggle
                        if (!isLegit) continue;

                        // Invoke with the first argument as the instance.
                        return method.Invoke(arguments[0], arguments.Skip(1).ToArray());
                    }
                }
                #endregion

                #region Field/property
                if (member is FieldInfo)
                {
                    // TODO add a "found field, but had arguments" message
                    if (arguments.Length != 1) continue;

                    // Simple get value.
                    return ((FieldInfo)member).GetValue(arguments[0]);
                }
                if (member is PropertyInfo)
                {
                    // Property access shouldn't have other args.
                    if (arguments.Length != 1) continue;

                    // TODO get public things, account for array index.
                    return ((PropertyInfo)member).GetValue(arguments[0]);
                }
                #endregion
            }

            // If we looped through everything without returning, throw exception.
            throw new MissingMemberException("Can't find a matching method!");
        }
    
        /// <summary>
        /// Special handling of getting generic types from assemblies.
        /// </summary>
        /// <param name="assem">The assembly to search</param>
        /// <param name="typeName">The name of the type</param>
        /// <param name="ignoreCase">Whether to ignore case</param>
        /// <returns></returns>
        public static Type GetTypeFromAssembly(Assembly assem, string typeName, bool ignoreCase)
        {
            foreach (var type in assem.DefinedTypes)
            {
                if (type.Name == typeName)
                {

                }
            }
            return null;
        }
    }

    /// <summary>
    /// Contains a layer of the stack of the runtime.
    /// </summary>
    class Scope
    {
        /// <summary>
        /// Defined variables in the current scope.
        /// </summary>
        public Dictionary<string, object> Variables;
        /// <summary>
        /// Defined functions in the current scope.
        /// </summary>
        public Dictionary<string, FunctionInfo> DefinedFunctions;

        public object GetVariableValue(string name)
        {
            var obj = Variables[name];

            if (obj == null) throw new Exception("hi!");

            var type = obj.GetType();

            //if (type.getN)

            return null;
        }

        public Scope()
        {
            Variables = new Dictionary<string, dynamic>();
        }
    }

    class FunctionInfo
    {

    }
}