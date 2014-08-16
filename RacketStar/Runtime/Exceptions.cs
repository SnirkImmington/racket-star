using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar.Runtime
{
    /// <summary>
    /// Base class for all exceptions thrown at runtime, with methods for capturing and tracking 
    /// the stack and metadata.
    /// </summary>
    abstract class RacketException : Exception
    {
        public string GetRuntimeErrorLog()
        {
            return null;
        }

        public RacketException(string message) : base(message) { }
    }
    
    /// <summary>
    /// 
    /// </summary>
    class DataNotFoundException : RacketException
    {
        public DataNotFoundException(string message) : base(message) { }
    }
    
    /// <summary>
    /// Thrown when a variable that does not exist is attempted to be referenced.
    /// </summary>
    class UndefinedVariableException : RacketException
    {
        public UndefinedVariableException(string message) : base(message) { }
    }

    /// <summary>
    /// Thrown when a contract is broken on a method.
    /// </summary>
    class ContractViolationException : RacketException
    {
        public ContractViolationException(string message) : base(message) { }
    }

    /// <summary>
    /// Called when syntax is invalid.
    /// </summary>
    class InvalidSyntaxException : RacketException
    {
        public InvalidSyntaxException(string message) : base(message) { }
    }

    #region RacketSharp

    /// <summary>
    /// Thrown when a method is invoked with invalid parameters.
    /// </summary>
    class InvalidTypeArgumentsException : RacketException
    {
        public InvalidTypeArgumentsException(string message) : base(message) { }
    }

    #endregion
}
