using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketInterpreter
{
    /// <summary>
    /// Base class for all syntax nodes. <br/>
    /// Each syntax node needs only to ultimately return a value.
    /// </summary>
    abstract class SyntaxNode
    {
        /// <summary>
        /// Each syntax node should ultimately return a value.
        /// </summary>
        /// <returns></returns>
        public abstract object getValue();
    }

    /// <summary>
    /// SyntaxNode for chars or strings or ints/floats/doubles
    /// which are known at compile time.
    /// </summary>
    class LiteralSyntaxNode : SyntaxNode
    {
        /// <summary>
        /// The type of the variable - we're still working on types.
        /// </summary>
        public string Type;
        /// <summary>
        /// The value of the variable - known at compile time -
        /// can be a string or char or number.
        /// </summary>
        public object Value;

        // We will compile "strings", 'strings', chars ('c'),
        // numbers as ints, floats or doubles for C# interopability
        public LiteralSyntaxNode(string type, object @value)
        {
            // value is a C# keyword, @value is a valid
            // parameter name, no other significance
            Type = type; Value = @value;
        }

        /// <summary>
        /// Returns the literal value
        /// </summary>
        /// <returns></returns>
        public override object getValue()
        {
            return Value;
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

        public override object getValue()
        {
            // push parameters to the state

            // Save the last for return
            // This is a cool one-liner but at some point we'll need more for like exceptions or something
            for (int i = 0; i < Parameters.Length - 1; i++)
                Invocations[i].getValue();

            return Invocations.Last().getValue();
        }
    }

    class FunctionInvocationSyntaxNode : SyntaxNode
    {
        public string Name;
        // Yea I feel like we'll have some subclass for these sometime
        public SyntaxNode[] Parameters;

        public FunctionInvocationSyntaxNode(string functionName, SyntaxNode[] parameters)
        {
            Name = functionName; Parameters = parameters;
        }

        public override object getValue()
        {

            // Some code to get a function that we/runtime defined.
            //var func = getFunctionByName(Name);

            // push name, parameters to state
            // Some code to invoke a function and lazily expand it/etc.
            // set parameters to values
            //return func.invoke(parameters);
            return null;
        }
    }

    class IfConditionalSyntaxNode : SyntaxNode
    {
        /// <summary>
        /// The expression to be checked against
        /// </summary>
        public SyntaxNode Conditional;
        /// <summary>
        /// The expression to be evaluated if Conditional is true
        /// </summary>
        public SyntaxNode TrueExpression; // length == 2
        /// <summary>
        /// The expression to be evaluated if Conditional is false
        /// </summary>
        public SyntaxNode FalseExpression;

        public IfConditionalSyntaxNode(SyntaxNode conditional, SyntaxNode trueExpression, SyntaxNode falseExpression)
        {
            Conditional = conditional; TrueExpression = trueExpression; FalseExpression = falseExpression;
        }
        public override object getValue()
        {
            return null;
        }
    }

    class ForLoopConditionalSyntax : ForConditionalSyntax
    {
        //public Function<int> maximum;
    }

    class ForConditionalSyntax : SyntaxNode
    {
        public SyntaxNode continueCheck;
    }

    class ForExpressionSyntax : SyntaxNode
    {
        public ForConditionalSyntax condition;
    }
	
}
