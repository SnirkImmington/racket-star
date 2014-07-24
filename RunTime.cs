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

        public static object GetVariableValue(string name)
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
            StringBuilder typeBuilder = new StringBuilder();

            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] == '-') break;
                typeBuilder.Append(name[i]);
            }

            return null;
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
