﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar.Runtime
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

    class UndefinedVariableException : RacketException
    {
        public UndefinedVariableException(string message) : base(message) { }
    }

    class ContractViolationException : RacketException
    {
        public ContractViolationException(string message) : base(message) { }
    }

    class InvalidSyntaxException : RacketException
    {
        public InvalidSyntaxException(string message) : base(message) { }
    }

    #region RacketSharp

    class InvalidTypeArgumentsException : RacketException
    {
        public InvalidTypeArgumentsException(string message) : base(message) { }
    }

    #endregion
}