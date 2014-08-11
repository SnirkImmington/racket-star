using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace RacketStar.Runtime
{
    /// <summary>
    /// Containing methods for searching .NET for stuff.
    /// </summary>
    static class Reflection
    {
        public static Type GetAnyTypeFromAssembly(Assembly assem, bool ignoreCase, string name)
        {
            foreach (var type in assem.GetExportedTypes())
            {
                // If the type is the same
                if (type.Name == name || (type.Name.StartsWith(name) && type.Name[type.Name.Length - 1] == '`'))
                    return type;
            }
            return null;
        }

        public static object GetGenericTypeFromAssembly(Assembly assem, string name, bool ignoreCase, int genericTypesLength)
        {
            return assem.GetType(string.Format("{0}`{1}`", name, genericTypesLength), false, ignoreCase);
        }

        

        /// <summary>
        /// Get the invocations/value of C# reflection info.
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
    }
}
