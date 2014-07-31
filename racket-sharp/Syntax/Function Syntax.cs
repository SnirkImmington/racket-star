using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace racket_sharp.Syntax
{
    /// <summary>
    /// Represents a function returned by the "define" keyword.
    /// This object will be added to the lookup tree to be later called
    /// via FunctionInvocationSyntaxNode
    /// </summary>
    class FunctionInfo
    {
        public string Name;
        public ParameterInfo[] Parameters;
        // Eventually switch this to an Expression object once that's properly defined. 
        public SyntaxNode[] Expressions;

        public FunctionInfo(string name, ParameterInfo[] parameters, SyntaxNode[] expressions)
        {
            Name = name; Parameters = parameters; Expressions = expressions;
        }

        public object GetValue()
        {
            // TODO add stuff to stack

            foreach (var expression in Expressions)
            {

            }

            // close stack
            return null;
        }
    }

    class ParameterInfo
    {
        public string Name;
        public string Type;
    }

    /// <summary>
    /// Syntax node used for (define (name arg arg) body body)
    /// </summary>
    class DefineDeclarationSyntaxNode : SyntaxNode
    {
        /// <summary>
        /// The name of the function, the first element in the second part.
        /// </summary>
        public string Name;
        /// <summary>
        /// The parameters to the method, to be added to the local stack.
        /// </summary>
        public ParameterInfo[] Parameters;
        /// <summary>
        /// The body syntecies that are executed with the last one being returned
        /// </summary>
        public SyntaxNode[] Invocations;

        public DefineDeclarationSyntaxNode(string Name, ParameterInfo[] parameters, SyntaxNode[] invotations)
        {
        }

        public override object GetValue()
        {
            // Return an object that represents the function
            // It can then be added to the state and later called using name/params
            return new FunctionInfo(Name, Parameters, Invocations);
        }
    }
}
