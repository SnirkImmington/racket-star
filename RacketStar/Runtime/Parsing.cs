﻿using System;
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
                // Eat sleep rave repeat
                var expressionEnd = FindExpression(text, i);
                nodes.Add(ParseExpression(text.Substring(i, expressionEnd)));
                i = expressionEnd;
            }
            return new CompileUnit(nodes.ToArray());
        }

        /// <summary>
        /// Creates a SyntaxNode from the given text.
        /// </summary>
        /// <param name="expression">Text to parse, should not contain start/finish parenthesis.</param>
        /// <returns>A parsed SyntaxNode.</returns>
        private static SyntaxNode ParseExpression(string expression)
        {
            // Split the node into arguments.
            var currStart = 0; // Use substrings instead of builders.
            // Save the args as text for parsing at the end.
            var strings = new List<string>();
            // Possible args
            var args = new List<SyntaxNode>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == ' ')
                {
                    strings.Add(expression.Substring(currStart, i-1));
                    currStart = i + 1;
                }
                // If there's a sub expression beginning
                else if (expression[i] == '(')
                {
                    // If there's an inner expression, can also assume that we're on the next arg.
                    strings.Add(expression.Substring(currStart, i - 1));
                    currStart = i + 1;

                    // Start searching from the beginning of the expression
                    var endIndex = FindExpression(expression, i+1);

                    // Parse the inner syntax node (from i to the end) and add to the args.
                    args.Add(ParseExpression(expression.SubstringIndex(i, endIndex)));

                    // Continue around the expression.
                    i = endIndex;
                }

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
