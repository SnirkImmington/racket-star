﻿using System;
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

                // We need to call findexpression with a pointer to the parenthesis ASAP because count starts at one.
                // That means we need to wait till we have the parenthesis to go in.

                var expressionEnd = FindExpression(text, i);
                // Don't include the first or last parenthesis here.
                nodes.Add(ParseExpression(text.Substring(i+1, expressionEnd-1)));
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
            var args = new List<SyntaxNode>(); int i;
            for (i = 0; i < expression.Length; i++)
            {
                var currChar = expression[i];
                var currStartValue = expression[currStart];
                if (expression[i] == ' ')
                {
                    args.Add(new TextSyntax(expression.SubstringIndex(currStart, i)));
                    currStart = i + 1;
                }
                // If there's a sub expression beginning
                else if (expression[i] == '(')
                {
                    // If there's an inner expression, can also assume that we're on the next arg.
                    if (i - currStart >  0)
                        args.Add(new TextSyntax(expression.SubstringIndex(currStart, i - 1)));

                    // Start searching from the beginning of the expression
                    var endIndex = FindExpression(expression, i+1);

                    // Parse the inner syntax node (from i+1 to the end) and add to the args.
                    // Use i + 1 here to avoid including the open parenthesis.
                    args.Add(ParseExpression(expression.SubstringIndex(i+1, endIndex)));

                    // Continue around the expression.
                    i = endIndex; currStart = i - 1;
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
                    //args.Add(new CommentSyntax(expression.SubstringIndex(i, ending)));
                    i = ending;
                }
            }

            // Add the rest of the text if it's not done
            if (i - currStart > 0)
                args.Add(new TextSyntax(expression.SubstringIndex(currStart, i)));

            // Handle no syntax nodes
            if (args.Count == 0) return null;



            // We've looked between the parenthesis. Time for some analysis
            return null;
        }

        private static string[] ParseSections(string input)
        {
            var turn = new List<string>(); 
            int i, currStart = 0;
            for (i = 0; i < input.Length; i++)
            {
                var currChar = input[i];
                var currStartValue = input[currStart];
                if (input[i] == ' ')
                {
                    turn.Add(input.SubstringIndex(currStart, i));
                    currStart = i + 1;
                }
                // If there's a sub expression beginning
                else if (input[i] == '(')
                {
                    // If there's an inner expression, can also assume that we're on the next arg.
                    if (i - currStart > 0)
                        turn.Add(input.SubstringIndex(currStart, i - 1));

                    // Start searching from the beginning of the expression
                    var endIndex = FindExpression(input, i + 1);

                    // Parse the inner syntax node (from i+1 to the end) and add to the args.
                    // Use i + 1 here to avoid including the open parenthesis.
                    turn.Add(input.SubstringIndex(i + 1, endIndex));

                    // Continue around the expression.
                    i = endIndex; currStart = i - 1;
                }
                // If there's a string there
                else if (input[i] == '"')
                {
                    var ending = FindStringEnding(input, i) + 1;
                    turn.Add(input.SubstringIndex(i, ending));
                    //turn.Add(new StringLiteralSyntax(Utils.GetEscapeString(input.SubstringIndex(i, ending))));
                    i = ending;
                }
                // Skip over comments
                else if (input[i] == ';')
                {
                    var ending = FindCommentEnding(input, i) + 1;
                    // TODO sort ;! docs comments and ; comments.
                    // Also, language differences!
                    //args.Add(new CommentSyntax(expression.SubstringIndex(i, ending)));
                    i = ending;
                }
            }

            // Add the rest of the text if it's not done
            if (i - currStart > 0)
                turn.Add(input.SubstringIndex(currStart, i));
            return turn.ToArray();
        }

        private static SyntaxNode ParseNode(string[] parts, LanguageDialect dialect)
        {
            var current = 0;
            
            #region If syntax
            if (parts[0] == "if")
            {
                // if <cond> <true> <false>
                // TODO make it smarter, identify parts, if <cond> <true> for no falses
                if (parts.Length != 4)
                    throw new InvalidSyntaxException("If statement had " + parts.Length + "instead of (if <condition> <true> <false>)!");

                return new IfConditionalSyntax(
                    ParseNode(ParseSections(parts[1]), dialect),  //  cond
                    ParseNode(ParseSections(parts[2]), dialect),  //  true
                    ParseNode(ParseSections(parts[3]), dialect)); // false
            }
            #endregion

            #region cond syntax
            if (parts[0] == "cond")
            {
                // cond [<condition> <statement>]
                // there are parenthesis around those that should have been
                // picked up (even though we're not looking for squares atm
                // so there should actully only be two parts.
                if (parts.Length != 2)
                    throw new InvalidSyntaxException("cond statement had " + parts.Length + " parts instead of (cond (<condition> <true> ...))!");

                var conds = new List<Tuple<SyntaxNode, SyntaxNode>>();
                var states = ParseSections(parts[1]);
                for (int i = 0; i < states.Length; i++)
                {
                    var subs = ParseSections(states[i]);
                    if (subs.Length != 2)
                        throw new InvalidSyntaxException("cond inner statement #" + (i + 1) + " had " + subs.Length + " parts instead of (cond (<condition> <true>))!");

                    var condition = ParseNode(ParseSections(subs[0]), dialect);
                    var value = ParseNode(ParseSections(subs[1]), dialect);

                    conds.Add(new Tuple<SyntaxNode, SyntaxNode>(condition, value));
                }
                return null; // TODO add cond syntax node.
            }
            #endregion

            #region define syntax
            else if (parts[0] == "define")
            {
                if (dialect == LanguageDialect.RacketSharp)
                {

                }
                else
                {

                }
            }
            #endregion

            #region let syntax
            else if (parts[0] == "let")
            {

            }
            #endregion

            #region for syntax
            else if (parts[0] == "for")
            {
                
            }
            #endregion

            #region lambda syntax
            #endregion

            #region while syntax
            #endregion

            #region invocation syntax
            #endregion

            #region variable syntax
            #endregion

        }

        private static SyntaxNode ParseAtom(string atom, LanguageDialect dialect)
        {
            if (atom[0] == '"')
            {
                switch (atom.LastChar())
                {
                    case '"':
                        // parse string
                        var escapedString = Utils.GetEscapeString(atom);
                        return new StringLiteralSyntax(escapedString);

                    case 'c':
                        // parse string, get char
                        var escapedCharString = Utils.GetEscapeString(atom);
                        if (escapedCharString.Length != 1) throw new ArgumentException("Attempted to parse a char, it was not of length 1!");
                        return new CharLiteralSyntax(escapedCharString[0]);

                    default: throw new ArgumentException("Tried to parse a string not ending with a quote!");
                }
            }

            string numberParse = atom.Replace("_", "");
            string numberSub = atom.Substring(0, numberParse.Length - 2);
            bool hasDec = atom.Contains('.');

            if (numberParse[numberParse.Length-1] == 'd')
            {
                double posDouble;
                if (double.TryParse(numberSub, out posDouble))
                {
                    return new LiteralSyntaxNode("double", posDouble);
                }
            }
            else if (numberParse[numberParse.Length-1] == 'f')
            {
                float posFloat;
                if (float.TryParse(numberSub, out posFloat))
                {
                    return new LiteralSyntaxNode("float", posFloat);
                }
            }
            else
            {
                int posInt;
                if (int.TryParse(numberParse, out posInt))
                {
                    return new LiteralSyntaxNode("int", posInt);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the index of the end of a current parenthesis node.
        /// CURRENTLY INCLUDES CLOSE PARENTHESIS
        /// handles text[0] == '('
        /// </summary>
        /// <param name="text">The text to parse</param>
        /// <param name="start">Where to start looking</param>
        /// <returns>The index of the close parenthesis.</returns>
        private static int FindExpression(string text, int start)
        {
            // Keep track of the current text, number
            // of open/close parenthesis, and where we are.
            int count = 1, i = start;
            if (text[start] == '(') i++;
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
        /// Gets the ending index of the string - goes to the quote location.
        /// </summary>
        public static int FindStringEnding(string expression, int stringStart)
        {
            bool isEscaped = false;
            if (expression[stringStart] == '"') stringStart++;
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

        /// <summary>
        /// Compiles parsed syntax - figures out method expressions/variable gets/etc
        /// </summary>
        /// <param name="nodes">The split text of the node</param>
        /// <param name="dialect">The langauge</param>
        private static SyntaxNode GetNodeFromTexts(SyntaxNode[] nodes, LanguageDialect dialect)
        {
            if (nodes[0] is TextSyntax)
            {
                var text = nodes[0].GetValue(false, dialect);
            }
            return null;
        }

        private static SyntaxNode GetNodeData()
        {
            return null;
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
