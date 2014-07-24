using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketSharp
{
    public static class Utils
    {
        public static string ToCapsCase(this string input)
        {
            if (input == null) return null;
            if (input.Length == 1) return input.ToUpper();
            return char.ToUpper(input[0]) + input.Substring(1);
        }

        public static string ToCSharpString(string racketString)
        {
            var subStrings = new List<string>(10);
            var builder = new StringBuilder();
            for (int i = 0; i < racketString.Length; i++)
            {
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
    }
}
