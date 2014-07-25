using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketSharp
{
    class Parsing
    {
        public static void ParseInput(string input)
        {
            while (true)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    // Cache data -> space/time
                    bool escaped = false, inString = false, inChar = false;
                    StringBuilder currString = new StringBuilder(), currChar = new StringBuilder();

                    // So if we're in a string, we should deal with it pronto
                    if (inString)
                    {
                        // If this is a quote, the string is over!
                        if (input[i] == '"')
                        {
                            var data = currString.ToString();
                            // Add the string to an evaluation list
                        }
                        
                    }
                }
            }
        }

        public static void ParseInput(string input, int value)
        {
            // Two iterations: one to remove unneeded characters, one to parse.
            LinkedList<string> strings = new LinkedList<string>(), comments = new LinkedList<string>();
            LinkedList<char> chars = new LinkedList<char>(), escapes = new LinkedList<char>();

            char lastEscape;
            bool inString = false, inComment = false, inChar = false, isEscaped = false;

            for (int i = 0; i < input.Length; i++)
            {
                if (inString)
                {
                    if (input[i] == '\\')
                    {
                        if (isEscaped)
                        {
                            isEscaped = false;

                            input = input.Remove(i - 1, 2);
                            input = input.Insert(i - 1, "");
                        }
                    }
                    else if (input[i] == '"')
                    {

                    }
                }

                if (inComment)
                {

                }
            }
        }
    
        public static void ParseInput(string input, bool val)
        {
            while (true)
            {
                // Check for strings/chars to parse around.
                bool isString = false, isChar = false, 
                // Remove comments from the code asap
                    isComment = false;
                int symbolStart = 0;
                
                for (int i = 0; i < input.Length; i++)
                {
                    if (isString && input[i] == '"' && input[i - 1] != '\\')
                    {
                        // the string is over
                        //input = input.Remove(symbolStart, i - symbolStart);
                        isString = false;
                    }
                    else if (isChar)
                    {
                        if (input[i] == '\'' && input[i - 1] != '\\')
                        {
                            // The char is over
                            //input = input.Remove(symbolStart, i - symbolStart);
                            isChar = false;
                        }
                    }
                    else if (isComment)
                    {
                        if (input[i] == '\n' || input[i] == '|')
                        {
                            // Remove the comment
                            //input = input.Remove(symbolStart, i - symbolStart);
                            isComment = false;
                        }
                        // else just move along
                    }
                    else // none of the above!
                    {
                        if (input[i] == ')')
                        {
                            // okay, now we can loop backwards, also keepying track of the chars!
                        }
                    }
                }
            }
        }
    
        public static ParseData ParseInput(string input, long val)
        {
            // Build new parse string
            var newString = new StringBuilder();
            // Build current comment/etc.
            var currData = new StringBuilder();
            int dataStart = 0;
            // Current blocker status
            ParseType type = ParseType.None;
            bool escapade = false;

            var strings = new List<string>();
            var chars = new List<char>();
            int currString = 0, currChar = 0;

            for (int i = 0; i < input.Length; i++)
            {
                #region switch(type)
                switch (type)
                {
                    #region case regular code:
                    case ParseType.None:
                        switch (input[i])
                        {
                            case '"':
                                type = ParseType.String;
                            break;

                            case '\'':
                                type = ParseType.Char;
                            break;

                            case '|':
                                type = ParseType.Comment;
                            break;

                            default:
                                newString.Append(input[i]);
                                break;
                        }
                    break;
                    #endregion

                    // Currently parsing string:
                    #region case ParseType.String:
                    case ParseType.String:
                        if (input[i] == '\\')
                        {
                            if (!escapade) escapade = true;
                            else
                            {
                                escapade = false;
                                currData.Append('\\');
                            }
                        }
                        else if (input[i] == '"')
                        {
                            if (escapade)
                            {
                                escapade = false;
                                currData.Append('"');
                            }
                            else // string end
                            {
                                type = ParseType.None;
                                newString.Append("$string:").Append(currString);
                                strings.Add(currData.ToString());
                                currString++; currData.Clear();
                            }
                        }
                        else 
                        {
                            if (!escapade) currData.Append(input[i]);
                            else
                            {
                                currData.Append(GetEscapeChar(input[i]));
                            }
                        }
                    break;
                    #endregion

                    case ParseType.Char:
                        
                    break;

                    case ParseType.Comment:
                        if (input[i] == '\n' || input[i] == '|')
                        {
                            type = ParseType.None;
                        }
                    break;

                } // switch
                #endregion
            }

            // Now we are done looking at this string already
            return new ParseData(newString.ToString(), strings, chars);
        }

        enum ParseType : byte
        {
            None = 0,
            Comment = 1,
            String,
            Char
        }

        private static char GetEscapeChar(char input)
        {
            switch (input)
            {
                case 't': return '\t';
                case 'r': return '\r';
                case 'n': return '\n';
                default: return 'j'; // no errors here either
            }
        }
    }

    class ParseData
    {
        public string NewCode { get; set; }
        public List<string> StringsRef;
        public List<char> CharsRef;

        public ParseData(string newCode, List<string> stringsRef, List<char> charsRef)
        {
            NewCode = newCode; StringsRef = stringsRef; CharsRef = charsRef;
        }
    }
}
