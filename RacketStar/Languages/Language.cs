using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar.Languages
{
    abstract class Language
    {
        public abstract string GetMethodReplacement(string method);
    }
}
