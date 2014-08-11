using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar.Workspace
{
    abstract class Scope
    {
        public Scope()
        {

        }
    }

    class RacketScope : Scope
    {
        //public Dictionary<string, ValueNode> Objects;


    }

    class CSharpScope : Scope
    {

    }

    class SnirkScope : Scope
    {

    }
}
