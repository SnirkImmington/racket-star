using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RacketStar.Runtime;


[Flags]
enum SomeFlagsSettings : byte
{
    beCareful = 1,
    ConserveAmmo = 2,
    Quiet = 4,
    BeSpeedy = 8
}

//var settings = SomeFlagsSettings.beCareful | SomeFlagsSettings.Quiet | SomeFlagsSetting.ConserveAmmo;
//if (settings.HasFlag(SomeFlagSettings.beCareful)) maxSpeed = 3;

namespace RacketStar
{
    /// <summary>
    /// Suprisingly, this contains utilities.
    /// </summary>
    public static class Utils
    {
        #region Special chars

        /// <summary>
        /// The char λ
        /// </summary>
        public const char Lambda = 'λ';

        /// <summary>
        /// The char ♯
        /// </summary>
        public const char Sharp = '♯';

        /// <summary>
        /// The char*
        /// </summary>
        public const char Star = '★';

        #endregion

        #region Languages

        /// <summary>
        /// Capitalizes a string.
        /// </summary>
        /// <param name="input">The string to capitalize</param>
        /// <returns>Capitalized string</returns>
        public static string ToCapsCase(this string input)
        {
            if (input == null) return null;
            if (input.Length == 1) return input.ToUpper();
            return char.ToUpper(input[0]) + input.Substring(1);
        }

        /// <summary>
        /// Gets an array of strings CapitalCased by underscores and split by dashes.
        /// </summary>
        /// <param name="racketString">The racket style string to parse</param>
        /// <example>
        /// "string-length" -> "String" "Length";
        /// "string_builder-length" -> "StringBuilder" "Length"
        /// </example>
        /// <returns>C# style strings, as if split by dots.</returns>
        public static string[] GetCSharpStrings(string racketString)
        {
            // Split into strings.
            var subStrings = racketString.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            // Convert each section.
            for (int i = 0; i < subStrings.Length; i++)
                subStrings[i] = ToCSharpString(subStrings[i]);

            return subStrings;
        }

        /// <summary>
        /// Capitalizes a racket-style underscored string.
        /// </summary>
        /// <param name="racketString">The string to capitalize</param>
        /// <returns>C# style string - 'string_format' -> "StringFormat"</returns>
        public static string ToCSharpString(string racketString)
        {
            var subStrings = new List<string>(10);
            var builder = new StringBuilder();
            for (int i = 0; i < racketString.Length; i++)
            {
                // Check for underscores in names
                if (racketString[i] == '_')
                {
                    subStrings.Add(builder.ToString().ToCapsCase());
                    builder.Clear();
                }
                else builder.Append(racketString[i]);

            }
            subStrings.Add(builder.ToString().ToCapsCase());
            builder.Clear();

            foreach (var text in subStrings) builder.Append(text);

            return builder.ToString();
        }

        public static string GetLanguageName(LanguageDialect dialect)
        {
            switch (dialect)
            {
                case LanguageDialect.RacketStar:
                    return "" + Lambda + Star;

                case LanguageDialect.RacketPrime:
                    return Lambda + "'";

                case LanguageDialect.RacketSharp:
                    return "" + Lambda + Sharp;

                case LanguageDialect.RacketSnirk:
                    return Lambda + "Snirk";

                // We've exhausted all possible LanguageDialect definitions
                default: return ":D";
            }
        }

        public static string GetFullLanguageName(LanguageDialect dialect)
        {
            return dialect.ToString();
        }

        #endregion

        #region String Operations

        /// <summary>
        /// Gets an "op_XXXX" or similar name for the given method.
        /// You can do methodName = GetOperatorString(methodName)
        /// in order to make sure it's legit.
        /// </summary>
        /// <param name="input">An operator string to convert.</param>
        /// <returns></returns>
        public static string GetOperatorString(string input, LanguageDialect dialect)
        {
            switch (input)
            {
                case "+": return "op_Addition";
                case "-": return "op_Subtraction";
                case "*": return "op_Multiplication";
                case "/": return "op_Division";

                case "++": return "op_Increment";
                case "--": return "op_Decrement";

                case ">": return "op_GreaterThan";
                case "<": return "op_LessThan";

                case "<<": return "op_LeftShift";
                case ">>": return "op_RightShift";

                case ">=": return "op_GreaterThanOrEqual";
                case "<=": return "op_LessThanOrEqual";

                case "set": return "op_Assign";

                case "equals": return "op_Equality";

                case "||":
                    if (dialect == LanguageDialect.RacketSharp)
                        return "op_Or"; break;

                case "&&":
                    if (dialect == LanguageDialect.RacketSharp)
                        return "op_And"; break;

                case "mod":
                    if (dialect != LanguageDialect.RacketSharp)
                        return "op_Modulus"; break;
                
                case "%":
                    if (dialect == LanguageDialect.RacketSharp)
                        return "op_Modulus"; break;

                case "bitand":
                    if (dialect != LanguageDialect.RacketSharp)
                        return "op_BitwiseAnd"; break;

                case "bitor":
                    if (dialect != LanguageDialect.RacketSharp)
                        return "op_BitwiseOr"; break;

                case "&":
                    if (dialect == LanguageDialect.RacketSharp)
                        return "op_BitWiseOr"; break;

                case "|":
                    if (dialect == LanguageDialect.RacketSharp)
                        return "op_BitWiseAnd"; break;

                case "^":
                    if (dialect != LanguageDialect.RacketSharp)
                        return "op_ExclusiveOr"; break;

                // If there's nothing wrong don't change it.
                default: return input;

            }
            
            return input;
        }

        /// <summary>
        /// Extension method for string.Format
        /// </summary>
        /// <param name="args">The args to format</param>
        public static string Format(this string input, params object[] args) { return string.Format(input, args); }

        /// <summary>
        /// Retrieves a substring from this instance.
        /// The substring starts at the specified character position and ends at the finish character position.
        /// If finish is greater than the length of the string an exception is thrown.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">If the index is greater than the length of the string</exception>
        /// <param name="input">Text to input</param>
        /// <param name="start">Starting position to check</param>
        /// <param name="finish">Last position to check</param>
        /// <returns></returns>
        public static string SubstringIndex(this string input, int start, int finish) 
        {
            if (finish > input.Length + 1) throw new ArgumentOutOfRangeException("The ending index must not be out of bounds of the string.");
            return input.Substring(start, finish - start); 
        }

        /// <summary>
        /// Gets an escaped version of a string. Can be used for chars (confirm that the length == 1).
        /// </summary>
        /// <param name="input">The text to input</param>
        /// <returns>An escaped version.</returns>
        public static string GetEscapeString(string input)
        {
            // Check for escaping and build the text
            var isEscaped = false;
            var builder = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                // If we're escaped, add the escape char.
                if (isEscaped)
                {
                    builder.Append(GetEscapeChar(input[i]));
                    isEscaped = false;
                }
                // If not, look for escape char.
                else if (input[i] == '\\')
                    isEscaped = true;
                // If neither append the char.
                else builder.Append(input[i]);
            }
            // Should not be escaped at the end of the char.
            if (isEscaped) { throw new ArgumentException("Input ended with escaped text!"); }

            return builder.ToString();
        }

        /// <summary>
        /// Gets an escape char for the input, or ! if invalid.
        /// </summary>
        /// <param name="input">The input to analyze</param>
        /// <returns>Escaped version of the char: 'n' -> '\n'</returns>
        public static char GetEscapeChar(char input)
        {
            switch (input)
            {
                case 'n': return '\n';
                case 't': return '\t';
                case 'r': return '\r';
                case '"': return '"';
                case '\'': return '\'';
                case '\\': return '\\';
                default: return '!';
            }
        }
        
        #endregion
    }
}
