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
        public override object GetValue(bool runTime, LanguageDialect dialect)
        {
            var conditional = Conditional.GetValue<bool>(runTime, dialect);

            if (conditional) return TrueExpression.GetValue(runTime, dialect);
            return FalseExpression.GetValue(runTime, dialect);
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
        public override object GetValue(bool runTime, LanguageDialect dialect)
        {
            // Should the max value be cached or reevaluated every time?
            object counter;
            var startMax = Maximum.GetValue<IComparable>(runTime, dialect);

            // Create new counter of proper type initialized to zero.
            counter = startMax.GetType().GetConstructor(Type.EmptyTypes).Invoke(null) as IComparable;

            while (true)
            {
                var comparison = startMax.CompareTo(counter);

                if (comparison == 0) return IterationOperation.GetValue(runTime, dialect);

                IterationOperation.GetValue(runTime, dialect);
            }
        }
    }

    /// <summary>
    /// Syntax for for loops
    /// </summary>
    class ForVariableDeclarationSyntax : SyntaxNode
    {
        public override object GetValue(bool runTime, LanguageDialect dialect)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Syntax for looping over a range 
    /// </summary>
    class ForRangeConditionalSyntax : ForLoopSyntax
    {
        public override object GetValue(bool runTime, LanguageDialect dialect)
        {
            throw new NotImplementedException();
        }
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

        public override object GetValue(bool runTime, LanguageDialect dialect)
        {
            // TODO for loops have their own stack. If runtime.
            object value = null;
            for (VariableCreationNode.GetValue(runTime, dialect); IncrementNode.GetValue<bool>(runTime, dialect); FinishNode.GetValue(runTime, dialect))
            {
                value = LoopBody.GetValue(runTime, dialect);
            }
            return value;
        }
    }
}
