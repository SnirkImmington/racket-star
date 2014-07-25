using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace RacketSharp
{
    static class RunTime
    {
        private static const BindingFlags METHOD_FLAGS = 
            BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.IgnoreReturn | BindingFlags.Static | BindingFlags.Instance

        public static LinkedList<Scope> Scopes = new LinkedList<Scope>();

        public static Dictionary<string, object> NativeMethods;

        public static LinkedListNode<Scope> Current;

        public static Type[] BASIC_TYPES = new Type[] { typeof(string), typeof(bool), typeof(int), typeof(char), typeof(float), typeof(double) };

        public static void Initialize()
        {
            Scopes = new LinkedList<Scope>();
            Scopes.AddFirst(new Scope());
            Current = Scopes.Last;
        }

        public static object GetVariableValue(string name, object[] arguments)
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

            // At this point, we need to search C# libs.
            // rakcet -> C# naming:
            // string-format -> String.Format
            // string_builder-new -> new StringBuilder (LAAATER)
            // (string_builder-length builder) -> StringBuilder.Length
            var names = Utils.GetCSharpStrings(name);

            // TODO keyword for instance methods!
            // (.length "hello world") -> "hello world".getType().getThingie("length")
            // (string-format "hello, {0}", "world")

            // If we can't parse the string properly for some reason at this point we're done.
            if (names.Length == 0) return null;

            string methodName = names.Last();
            Type type = null;

            // Loop through all but the last method.
            for (int i = 0; i < names.Length - 1; i++)
            {
                // If the type doesn't exist, it's the first time, get the base type.
                if (type == null)
                {
                    // Search the type system for the type, using the System prefix.
                    // TODO add in searches for referenced assemblies.
                    type = Type.GetType("System." + names[0], false, false); // TODO WE CAN IGNORE CASE!!!1!!!
                    // TODO exceptions
                    if (type == null) throw new TypeAccessException("Could not get type");
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

            // Get method information.
            // TODO make arguments and types clearer, since arguments have values
            // we are quite capable of doing that.
            MethodInfo methodInfo = null;
            try
            {
                methodInfo = type.GetMethod(methodName, METHOD_FLAGS);
            }
            catch (AmbiguousMatchException ex)
            {
                var members = type.GetMember(methodName);
                
                foreach (var member in members)
                {
                    if (member is MethodInfo)
                    {
                        var method = member as MethodInfo;

                        // For now, ignore generic methods.
                        if (method.IsGenericMethod) continue;
                        var parameters = method.GetParameters();

                        // Match not found.
                        if (parameters.Length != arguments.Length) continue;

                        // break 2;
                        bool shouldBreak = false;

                        for (int i = 0; i < parameters.Length; i++)
                        {
                            if (!arguments[i].GetType().IsAssignableFrom(arguments[i].GetType()))
                            {
                                shouldBreak = true; break;
                            }
                        }
                        if (shouldBreak) break;
                        
                        // else we have found a method in method.

                        if (method.IsStatic)
                        {
                            return method.Invoke(null, arguments);
                        }
                        else // method is instance
                        {
                            return method.Invoke(arguments[0], arguments.Skip(1).ToArray());
                        }
                    }
                    else if (member is FieldInfo)
                    {
                        var field = member as FieldInfo;
                    }
                    else if (member is PropertyInfo)
                    {
                        var prop = member as PropertyInfo;
                    }
                }
            }
            // Note: this did not work the first time.
                //null, arguments.ToList().ConvertAll(a => a.GetType()).ToArray(), null);
            
            // TODO SEARCH FOR PROPERTIES AND FIELDS AS WELL!!!!
            if (methodInfo == null) throw new MissingMethodException("No method found!");

            if (methodInfo.IsGenericMethod) throw new NotImplementedException("Type generics are not implemented at this time.");

            // TODO add constructor search!!
            // Should return a ConstructorInfo instead and be eval'd first if methodName == new.
            if (methodInfo.IsConstructor)
            {
                return null; // ((ConstructorInfo)methodInfo).Invoke(arguments);
            }
            else if (methodInfo.IsStatic)
            {
                return methodInfo.Invoke(null, arguments);
            }
            else // methodInfo is instance
            {
                return methodInfo.Invoke(arguments[0], arguments.Skip(1).ToArray());
            }

            //return null;
        }

        public static FunctionInfo GetFunction(string name) { return null; }

        public static object CallMethod(string methodName, object[] arguments)
        {
            // 1. Search local scope(s).

            // Search down the scopes.
            var func = GetFunction(methodName);
            if (func != null)
            {
                // Call function value with parameters.
            }

            // else, we need to search .NET for it.

            // 2. Create C# type info - parse name.
            // Example: some_class-sub_class-some_method.
            // -> method = SomeClass.SubClass.SomeMethod 
            // if (method.static) method(arguments) else arguments[0].method(arguments.subList(1));
            var dashSplit = methodName.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            Type type = null; MethodInfo method = null;

            return null;
        }
    }

    class Scope
    {
        public Dictionary<string, object> Variables;
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
}
