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
    class ForLoopConditionalSyntax : ForLoopSyntax
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

    /// <summary>
    /// Syntax for for loops
    /// </summary>
    class ForVariableDeclarationSyntax : SyntaxNode
    {

    }

    /// <summary>
    /// Syntax for looping over a range 
    /// </summary>
    class ForRangeConditionalSyntax : ForLoopSyntax
    {
        
    }

    /// <summary>
    /// For syntax with three parts:
    /// variable creation
    /// loop check
    /// loop action
    /// </summary>
    class ForCStyleSyntax : ForLoopSyntax
    {
        /// <summary>
        /// Syntax of first part of node (i = 0)
        /// </summary>
        public SyntaxNode VariableCreationNode;
        /// <summary>
        /// Syntax of second part of node (i &lt; length)
        /// </summary>
        public SyntaxNode IncrementNode;
        /// <summary>
        /// Syntax of third part of node (i++)
        /// </summary>
        public SyntaxNode FinishNode;

        /// <summary>
        /// Syntax of the loop body.
        /// </summary>
        public SyntaxNode LoopBody;

        public override object GetValue()
        {
            object value = null;
            for (VariableCreationNode.GetValue(); (bool)IncrementNode.GetValue(); FinishNode.GetValue())
            {
                value = LoopBody.GetValue();
            }
            return value;
        }
    }
}
