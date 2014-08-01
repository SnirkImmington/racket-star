using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace racket_sharp
{
    /// <summary>
    /// Syntax node with specific value for testing.
    /// </summary>
    class ValueTestingSyntaxNode : SyntaxNode
    {
        public object value;
        public override object GetValue()
        {
            return value;
        }
        public ValueTestingSyntaxNode(object theValue)
        {
            value = theValue;
        }
    }
}
