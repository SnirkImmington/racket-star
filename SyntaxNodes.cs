using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketSharp
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
        public abstract object GetValue();
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
        public override object GetValue()
        {
            return Value;
        }
    }

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

    class FunctionInvocationSyntaxNode : SyntaxNode
    {
        public string Name;
        // Yea I feel like we'll have some subclass for these sometime
        public SyntaxNode[] Parameters;

        public FunctionInvocationSyntaxNode(string functionName, SyntaxNode[] parameters)
        {
            Name = functionName; Parameters = parameters;
        }

        public override object GetValue()
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
        public override object GetValue()
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

        public override object GetValue()
        {
            // Check continueCheck
            var shouldContinueObj = continueCheck.GetValue();

            if (!(shouldContinueObj is bool))
                throw new Exception("Cmon man");

            // Casting is done later.
            return shouldContinueObj;
        }
    }

    class ForExpressionSyntax : SyntaxNode
    {
        public ForConditionalSyntax condition;

        public override object GetValue()
        {
            while (true)
            {
                var value = condition.GetValue()
            }
        }
    }
	
}
