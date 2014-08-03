using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace racket_sharp
{
    abstract class RacketException : Exception
    {
        public string GetRuntimeErrorLog()
        {
            return null;
        }

        public RacketException(string message) : base(message) { }
    }

    class DataNotFoundException : RacketException
    {
        public DataNotFoundException(string message) : base(message) { }
    }

    class ContractViolationException : RacketException
    {

    }

    class InvalidSyntaxException : RacketException
    {
        public InvalidSyntaxException(string message) : base(message) { }
    }
}
