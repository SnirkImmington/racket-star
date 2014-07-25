using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketSharp
{
    class Conversions
    {
        /// <summary>
        /// Converts a CSharpTypeName to a racket-type-name.
        /// </summary>
        /// <param name="cSharpName">The name to convert</param>
        /// <returns>A lower, dashier version.</returns>
        public static string getRacketName(string cSharpName)
        {
            var currentWordBuilder = new StringBuilder();
            var totalWordBuilder = new StringBuilder();

            for (int i = 0; i < cSharpName.Length; i++)
            {
                if (char.IsUpper(cSharpName[i]) && i > 0) // cSharpName[i].isUpper
                {
                    // Stop checking the thing
                    totalWordBuilder.Append(currentWordBuilder.ToString()).Append('-');
                    currentWordBuilder.Clear();

                }
                // Make the character lowercase
                currentWordBuilder.Append(char.ToLower(cSharpName[i]));

            }

            if (currentWordBuilder.Length > 0)
                totalWordBuilder.Append(currentWordBuilder.ToString());

            var text = totalWordBuilder.ToString();

            if (text.EndsWith("-"))
                return text.Substring(0, text.Length - 2);
            return text;
        }

        public static string getCSharpName(string racketSharpName, string variableType) //make that an enum
        {
            var currentWordBuilder = new StringBuilder();
            var totalWordBuilder = new StringBuilder();
            // Check if need to capitalize
            bool isNewWord;
            // Naming conventions are different for functions and variables in C#
            if (variableType.equals("function"))
                isNewWord = true;
            else if (variableType.equals("variable"))
                isNewWord = false;
            else:
                isNewWord = true; // An enum should solve this particular possibility (error)

            for (int i = 0; i < racketSharpName.Length; i++)
            {
                if (char.IsLower(racketSharpName[i]) && isNewWord)
                {
                    // Stop checking the string
                    currentWordBuilder.Append(char.ToLower(racketSharpName[i]));
                    isNewWord = false;
                }

                else if (racketSharpName.Equals('_')
                {
                    // Makes the next character uppercase
                    isNewWord = true;
                }

                else if (racketSharpName.equals('-')
                {
                    // Makes the character a period because we are going to access an attribute of the variable
                    currentWordBuilder.Append('.');
                    isNewWord = true;
                }
                else
                    currentWordBuilder.Append(racketSharpName[i]);
            }

            if (currentWordBuilder.Length > 0)
                totalWordBuilder.Append(currentWordBuilder.ToString());

            var text = totalWordBuilder.ToString();
            return text;
        }
    }
}
