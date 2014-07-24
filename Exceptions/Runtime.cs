using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketSharp
{
    class ValueNotFoundException : Exception
    {
        public string VariableName;

        public ValueNotFoundException(string variableName) 
        { 
            VariableName = variableName; 
        }

        public override string Message
        {
            get { return "Unknown variable " + VariableNames; } 
        }
    }
}
