using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar.Languages
{
    class RacketSharp : Language
    {
        public override string GetMethodReplacement(string input)
        {
            switch (input)
            {
                // Support for x++/x--: usage in for loop style syntax
                case "++": return "op_Increment";
                case "--": return "op_Decrement";

                case ">>": return "op_LeftShift";
                case "<<": return "op_RightShift";

                case "==": return "op_Equal";
                case "set": return "op_Assign";

                default: return input;
            }
        }
    }
}
