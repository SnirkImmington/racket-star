using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar.Runtime
{
    abstract class CompilationException : Exception
    {
        public int StartIndex;
        public int EndIndex;

        public CompilationException(int start, int finish, string message) : base(message)
        { 
            StartIndex = start; EndIndex = finish;
        }
    }

    class MissingParenthesisException : CompilationException
    {
        public MissingParenthesisException(int start, int finish, string message)
            : base(start, finish, message) { }
    }
}
