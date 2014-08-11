using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar.Workspace
{
    /// <summary>
    /// Basic documentation: just has an info string.
    /// </summary>
    class BasicDocumentation
    {
        /// <summary>
        /// A summary of the code.
        /// </summary>
        public string Info;

        /// <summary>
        /// Basic constructor.
        /// </summary>
        public BasicDocumentation(string info)
        {
            Info = info;
        }

        // TODO conversion with .NET reflection
    }

    /// <summary>
    /// Provides documentation for methods, parameters, exceptions, and more.
    /// </summary>
    class MethodDocumentation : BasicDocumentation
    {
        /// <summary>
        /// A collection of parameter names and their info.
        /// </summary>
        public Dictionary<string, string> ParameterInfo;
        /// <summary>
        /// A collection of exception names and their causes.
        /// </summary>
        public Dictionary<string, string> ExceptionInfo;
        /// <summary>
        /// An extra bit about the return value.
        /// </summary>
        public string ReturnInfo;
        /// <summary>
        /// An extra bit for example code.
        /// </summary>
        public string Example;

        /// <summary>
        /// Constructor for method documentation with just info.
        /// </summary>
        /// <param name="info">Info about the method</param>
        public MethodDocumentation(string info) : base(info) { }

        /// <summary>
        /// Constructor with all fields filled.
        /// </summary>
        /// <param name="info">Information about the method</param>
        /// <param name="parameters">Parameters and information about each</param>
        /// <param name="exceptions">Exceptions and info about them</param>
        /// <param name="return">Return value extra bit</param>
        /// <param name="example">Example extra bit</param>
        public MethodDocumentation(string info, Dictionary<string, string> parameters, 
            Dictionary<string, string> exceptions, string @return, string example) : base(info)
        {
            ParameterInfo = parameters;
            ExceptionInfo = exceptions;
            ReturnInfo = @return;
            Example = example;
        }
        
        // TOOD conversion with .NET reflection
    }
}
