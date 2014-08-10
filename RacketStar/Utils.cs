using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar
{
    /// <summary>
    /// Suprisingly, this contains utilities.
    /// </summary>
    public static class Utils
    {
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
        /// <param name="racketString"></param>
        /// <returns></returns>
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

        public static string Format(this string input, params object[] args) { return string.Format(input, args); }

        /// <summary>
        /// Retrieves a substring from this instance.
        /// The substring starts at the specified character position and ends at the finish character position.
        /// If finish is greater than the length of the string an exception is thrown.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">If the index is greater than the length of the string</exception>
        /// <param name="input"></param>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        public static string SubstringIndex(this string input, int start, int finish) 
        {
            if (finish > input.Length + 1) throw new ArgumentOutOfRangeException("The ending index must not be out of bounds of the string.");
            return input.Substring(start, finish - start + 1); 
        }
    }
}
