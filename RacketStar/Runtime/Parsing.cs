using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar.Runtime
{
    static class Parsing
    {
        public static CompileUnit ParseLine(string text)
        {
            var nodes = new List<SyntaxNode>();
            var current = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ')')
                {
                    for (int tillOpen = i; tillOpen >= 0; tillOpen++)
                    {
                        if (text[tillOpen] == '(')
                        {
                            var node = ParseExpression(text.Substring(tillOpen, i - tillOpen + 1));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates a SyntaxNode from the given text.
        /// </summary>
        /// <param name="expression">Text to parse, should not contain start/finish parenthesis.</param>
        /// <returns>A parsed SyntaxNode.</returns>
        private static SyntaxNode ParseExpression(string expression)
        {
            // Split the node into arguments.
            var current = new StringBuilder();
            var args = new List<string>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == ' ')
                {

                }
                else if (expression[i] == '(')

            }
        }

        /// <summary>
        /// Gets the index of the end of a current parenthesis node.
        /// </summary>
        /// <param name="text">The text to parse</param>
        /// <param name="start">Where to start looking</param>
        /// <returns>The index of the close parenthesis.</returns>
        private static int FindExpression(string text, int start)
        {
            // Keep track of the current text, number
            // of open/close parenthesis, and where we are.
            int count = 1, i = start;
            for (; i < text.Length; i++)
            {
                // Keep track of how close we are 
                // to being finished with the node
                if (text[i] == '(') count++;
                else if (text[i] == ')') count--;

                // If we've matched the total parenthesis count
                // return the index we fuond.
                if (count == 0) return i;
            }

            // The text has been parsed but no matching parenthesis
            // Note that this should not happen in the IDE as it will match parenthesis
            throw new MissingParenthesisException(start, i, "Unable to finish parsing method starting at " + start + ".");
        }
    }

    class CompileUnit
    {
        SyntaxNode[] CompiledNodes;

        public CompileUnit(SyntaxNode[] nodes)
        {
            CompiledNodes = nodes;
        }
    }
}
