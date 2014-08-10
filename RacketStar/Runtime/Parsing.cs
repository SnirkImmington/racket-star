using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar.Runtime
{
    static class Parsing
    {
        public static CompileUnit ParseLine(string text)
        {
            var nodes = new List<SyntaxNode>();
            StringBuilder current = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ')')
                {
                    for (int tillOpen = i; tillOpen >= 0; tillOpen++)
                    {
                        if (text[tillOpen] == '(')
                    }
                }
            }
        }

        public static SyntaxNode ParseExpression(string expression)
        {

        }
    }

    class CompileUnit
    {
        SyntaxNode[] CompiledNodes;

        public CompileUnit(SyntaxNode[] nodes)
        {
            CompiledNodes = nodes;
        }
    }
}
