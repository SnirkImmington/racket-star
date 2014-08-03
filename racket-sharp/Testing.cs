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
        public override object GetValue(bool runTime, LanguageDialect dialect)
        {
            return value;
        }
        public ValueTestingSyntaxNode(object theValue)
        {
            value = theValue;
        }
    }

    class NestingClass<O>
    {
        /// <summary>
        /// Test class for generic arguments in reflection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        class TestClass<T> where T : new()
        {
            public static T getValue<D>()
            {
                return new T();
            }
        }
    }
}
