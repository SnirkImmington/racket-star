using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketSharp
{
    static class RunTime
    {
        public static LinkedList<Scope> Scopes = new LinkedList<Scope>();
    }

    class Scope
    {
        public Dictionary<string, object> Variables;

        public object GetVariableValue(string name)
        {
            var obj = Variables[name];

            if (obj == null) throw new Exception("hi!");

            return null;
        }

        public Scope()
        {
            Variables = new Dictionary<string, dynamic>();
        }
    }
}
