﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq.Expressions;

namespace RacketStar
{
    /// <summary>
    /// Syntax node with specific value for testing.
    /// </summary>
    class ValueTestingSyntaxNode : SyntaxNode
    {
        /// <summary>
        /// The value of the testing syntax node
        /// </summary>
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
}
