using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar.Languages
{
    class RacketPrime : Language
    {
        public static bool __boolean(object input)
        {
            return input is bool;
        }

        public static string GetTypeMatch(string typeName)
        {
            switch (typeName)
            {
                case "boolean": return "bool";
            }
            return "";
        }

        public override string GetMethodReplacement(string method)
        {
            throw new NotImplementedException();
        }
    }
}
