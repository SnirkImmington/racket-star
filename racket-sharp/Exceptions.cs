using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace racket_sharp
{
    class DataNotFoundException : Exception
    {
        public string SearchText;

        public DataNotFoundException(string text)
        {
            SearchText = text;
        }
    }

    class ContractViolationException : ArgumentException
    {

    }

    
}
