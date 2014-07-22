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

      public ConditionalSyntax(SyntaxNode conditional, SyntaxNode trueExpression, SyntaxNode falseExpression)
      {
         Conditional = conditional; TrueExpression = trueExpression; FalseExpression = falseExpression;
      }
    }
}
