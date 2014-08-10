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

        private static string FindExpression(string text, int start)
        {
            var count = 1;
            var builder = new StringBuilder();
            for (int i = start; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case '(': count++; break;
                    case ')': count--; break;
                    default: builder.Append(text[i]); break;
                }

                if (count == 0) return builder.ToString();
            }
            // Throw exception?
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
