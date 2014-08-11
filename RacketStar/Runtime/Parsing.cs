using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar.Runtime
{
    /// <summary>
    /// Contains methods for parsing text into racket
    /// </summary>
    static class Parsing
    {
        /// <summary>
        /// Parses the input line or possibly a file.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>A compileUnit of the found expressions</returns>
        public static CompileUnit ParseLine(string text)
        {
            var nodes = new List<SyntaxNode>();
            var current = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                // TODO
                // 1. Watch for comments
                // 2. Watch for and apply racketdocs
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
            // Possible args
            var args = new List<SyntaxNode>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == ' ')
                {
                    args.Add(new TextSyntax(expression.Substring(currStart, i-1)));
                    currStart = i + 1;
                }
                // If there's a sub expression beginning
                else if (expression[i] == '(')
                {
                    // If there's an inner expression, can also assume that we're on the next arg.
                    args.Add(new TextSyntax(expression.Substring(currStart, i - 1)));

                    // Start searching from the beginning of the expression
                    var endIndex = FindExpression(expression, i+1);

                    // Parse the inner syntax node (from i to the end) and add to the args.
                    args.Add(ParseExpression(expression.SubstringIndex(i, endIndex)));

                    // Continue around the expression.
                    i = endIndex; currStart = i;
                }
                // If there's a string there
                else if (expression[i] == '"')
                {
                    var ending = FindStringEnding(expression, i) + 1;
                    args.Add(new StringLiteralSyntax(Utils.GetEscapeString(expression.SubstringIndex(i, ending))));
                    i = ending;
                }
                // Skip over comments
                else if (expression[i] == ';')
                {
                    var ending = FindCommentEnding(expression, i) + 1;
                    // TODO sort ;! docs comments and ; comments.
                    // Also, language differences!
                    args.Add(new CommentSyntax(expression.SubstringIndex(i, ending)));
                    i = ending;
                }

            }
            // We've looked between the parenthesis. Time for some analysis
            return null;
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
                // TODO decouple this code
                // Skip over strings
                if (text[i] == '"') i = FindStringEnding(text, i) + 1;
                // Skip over comments
                else if (text[i] == ';') i = FindCommentEnding(text, i);

                // Keep track of how close we are 
                // to being finished with the node
                else if (text[i] == '(') count++;
                else if (text[i] == ')') count--;

                // If we've matched the total parenthesis count
                // return the index we fuond.
                if (count == 0) return i;
            }

            // The text has been parsed but no matching parenthesis
            // Note that this should not happen in the IDE as it will match parenthesis
            throw new MissingParenthesisException(start, i, "Unable to finish parsing method starting at " + start + ".");
        }

        private static int FindNextChar(char toFind)
        {
            return 0;
        }

        /// <summary>
        /// Goes from char to char, skipping over string literals.
        /// Gets the ending index of the string.
        /// </summary>
        public static int FindStringEnding(string expression, int stringStart)
        {
            bool isEscaped = false;
            for (int i = stringStart; i < expression.Length; i++)
            {
                // Enable escaping if it's disabled, or disable it if enabled
                if (expression[i] == '\\') isEscaped = !isEscaped;

                else if (expression[i] == '"' && !isEscaped) return i;

                else isEscaped = false;
            }
            throw new ArgumentOutOfRangeException("String never terminated!!!");
        }

        /// <summary>
        /// Goes to the end of the line
        /// </summary>
        /// <param name="expression">The text to parse</param>
        /// <param name="commentStart">The start of the comment</param>
        /// <returns>Next starting index of the text</returns>
        private static int FindCommentEnding(string expression, int commentStart)
        {
            return expression.IndexOf('\n', commentStart);
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

    class SyntaxInfo
    {

    }
}
