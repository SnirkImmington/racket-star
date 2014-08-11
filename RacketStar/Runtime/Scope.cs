using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar.Runtime
{
    /// <summary>
    /// Scope is used by the runtime to create and resolve variables.
    /// </summary>
    class Scope
    {
        /// <summary>
        /// Defined variables in the current scope.
        /// </summary>
        private Dictionary<string, dynamic> variables;
        /// <summary>
        /// Defined functions in the current scope.
        /// </summary>
        private Dictionary<string, FunctionInfo> definedFunctions;

        public object GetVariable(string name)
        {
            var obj = variables[name];

            if (obj == null) throw new UndefinedVariableException(name);

            var type = obj.GetType();

            //if (type.getN)

            return null;
        }

        public Scope()
        {
            variables = new Dictionary<string, dynamic>();
            definedFunctions = new Dictionary<string, FunctionInfo>();
        }
    }
}
