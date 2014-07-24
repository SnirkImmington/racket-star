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
            StringBuilder currentWordBuilder = new StringBuilder();
            StringBuilder totalWordBuilder = new StringBuilder();

            for (int i = 0; i < cSharpName.Length; i++)
            {
                if (char.IsUpper(cSharpName[i]) && i > 0) // cSharpName[i].isUpper
                {
                    // Stop checking the thing
                    totalWordBuilder.Append(currentWordBuilder.ToString()).Append('-');
                    currentWordBuilder.Clear();
                    currentWordBuilder.Append(char.ToLower(cSharpName[i]));
                }
                else // lowercase char
                {
                    currentWordBuilder.Append(char.ToLower(cSharpName[i]));
                }
            }

            if (currentWordBuilder.Length > 0)
                totalWordBuilder.Append(currentWordBuilder.ToString());

            var text = totalWordBuilder.ToString();

            if (text.EndsWith("-"))
                return text.Substring(0, text.Length - 2);
            return text;
        }
    }
}
