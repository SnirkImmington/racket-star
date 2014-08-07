using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar.Dialects
{
    abstract class Language
    {
        public abstract string Name { get; }

        public abstract Version CurrentVersion { get; }



    }
}
