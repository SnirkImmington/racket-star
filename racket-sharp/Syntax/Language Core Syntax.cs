using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace racket_sharp
{
    class IfConditionalSyntaxNode : SyntaxNode
    {
        /// <summary>
        /// The expression to be checked against
        /// </summary>
        public SyntaxNode Conditional; // (equals length 2)
        /// <summary>
        /// The expression to be evaluated if Conditional is true
        /// </summary>
        public SyntaxNode TrueExpression; //
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
    /// Provides special syntax to loop with a number.
    /// </summary>
    class ForLoopConditionalSyntax : ForConditionalSyntax
    {
        /// <summary>
        /// Number for maximum value
        /// </summary>
        public SyntaxNode Maximum;

        /// <summary>
        /// Body of loop
        /// </summary>
        public SyntaxNode IterationOperation;

        public override object GetValue()
        {
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
                var value = condition.GetValue();
            }
        }
    }
}
