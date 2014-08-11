using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar.Runtime
{
    abstract class FunctionInfo
    {
        public string Name { get; private set; }

        public abstract object Invoke(LanguageDialect langauge, object[] parameters);


        public FunctionInfo(string name)
        {

        }
    }
}
