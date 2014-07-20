﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketInterpreter
{
    struct TextSpan
    {
        private int start;
        private int end;

        public bool Contains(int index)
        {
            //return index >= start && index <= end;
            return start <= index <= end;
        }

        public int GetLength() { return end - start; }

        public TextSpan(int begin, int finish)
        {
            start = begin; end = finish;
        }
    }
}
