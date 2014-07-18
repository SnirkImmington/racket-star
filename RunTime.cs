using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketInterpreter
{
    static class RunTime
    {
        // Store global variables by 
        private static Dictionary<string, object> globals;
        //private static Dictionary<string, > functions;

        public static void Initialize()
        {
            globals = new Dictionary<string, object>();
        }

        public static object GetVariable(string name)
        {
            return globals[name];
        }

        public static void AddVariable(string name, object value)
        {
            globals.Add(name, value);
        }
    }
}
