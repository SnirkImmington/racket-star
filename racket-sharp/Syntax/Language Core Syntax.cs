using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace racket_sharp
{
    /// <summary>
    /// Syntax node for if statements.
    /// </summary>
    [Serializable]
    class IfConditionalSyntax : SyntaxNode
    {
        /// <summary>
        /// The expression to be checked against
        /// </summary>
        public SyntaxNode Conditional;
        /// <summary>
        /// The expression to be evaluated if Conditional is true
        /// </summary>
        public SyntaxNode TrueExpression;
        /// <summary>
        /// The expression to be evaluated if Conditional is false
        /// </summary>
        public SyntaxNode FalseExpression;

        /// <summary>
        /// Constructor with assign-arguments.
        /// </summary>
        public IfConditionalSyntax(SyntaxNode conditional, SyntaxNode trueExpression, SyntaxNode falseExpression)
        {
            Conditional = conditional; TrueExpression = trueExpression; FalseExpression = falseExpression;
        }
        
        /// <summary>
        /// Evaluates the if - conditional == true ? TrueExpression : FalseExpression
        /// </summary>
        public override object GetValue()
        {
            var conditionalObj = Conditional.GetValue();

            if (conditionalObj is bool)
            {
                if ((bool)conditionalObj)
                    return TrueExpression.GetValue();
                return FalseExpression.GetValue();
            }
            throw new InvalidOperationException("Conditional statement must be boolean");
        }
    }

    /// <summary>
    /// Abstract class for all for loop syntaxes.
    /// </summary>
    abstract class ForLoopSyntax : SyntaxNode
    {
        // TODO something for iterators?
    }

    /// <summary>
    /// Provides special syntax to loop with a number.
    /// </summary>
    [Serializable]
    class ForLoopConditionalSyntax
    {
        /// <summary>
        /// Number for maximum value
        /// </summary>
        public SyntaxNode Maximum;

        /// <summary>
        /// Body of loop
        /// </summary>
        public SyntaxNode IterationOperation;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object GetValue()
        {
            // Should the max value be cached or reevaluated every time?
            object counter;
            var startMax = (IComparable)Maximum.GetValue();

            // Create new counter of proper type initialized to zero.
            counter = startMax.GetType().GetConstructor(Type.EmptyTypes).Invoke(null) as IComparable;

            while (true)
            {
                var comparison = startMax.CompareTo(counter);

                if (comparison == 0) return IterationOperation.GetValue();

                IterationOperation.GetValue();
            }
        }
    }
}
