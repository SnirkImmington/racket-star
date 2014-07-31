using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace racket_sharp
{
    /// <summary>
    /// Base class for all syntax nodes. <br/>
    /// Each syntax node needs only to ultimately return a value.
    /// </summary>
    abstract class SyntaxNode
    {
        /// <summary>
        /// Each syntax node should ultimately return a value.
        /// </summary>
        /// <returns></returns>
        public abstract object GetValue();
    }

    /// <summary>
    /// SyntaxNode for chars or strings or ints/floats/doubles
    /// which are known at compile time.
    /// </summary>
    class LiteralSyntaxNode : SyntaxNode
    {
        /// <summary>
        /// The type of the variable - we're still working on types.
        /// </summary>
        public string Type;
        /// <summary>
        /// The value of the variable - known at compile time -
        /// can be a string or char or number.
        /// </summary>
        public object Value;

        // We will compile "strings", 'strings', chars ('c'),
        // numbers as ints, floats or doubles for C# interopability
        public LiteralSyntaxNode(string type, object @value)
        {
            // value is a C# keyword, @value is a valid
            // parameter name, no other significance
            Type = type; Value = @value;
        }

        /// <summary>
        /// Returns the literal value
        /// </summary>
        /// <returns></returns>
        public override object GetValue()
        {
            return Value;
        }
    }

    /// <summary>
    /// Syntax of function invocations -
    /// ((+ (string-length text) 3)
    /// </summary>
    class FunctionInvocationSyntaxNode : SyntaxNode
    {
        /// <summary>
        /// The name of the function being invoked.
        /// </summary>
        public string Name;
        // Yea I feel like we'll have some subclass for these sometime
        public SyntaxNode[] Parameters;

        public FunctionInvocationSyntaxNode(string functionName, SyntaxNode[] parameters)
        {
            Name = functionName; Parameters = parameters;
        }

        public override object GetValue()
        {

            // Some code to get a function that we/runtime defined.
            //var func = getFunctionByName(Name);

            // push name, parameters to state
            // Some code to invoke a function and lazily expand it/etc.
            // set parameters to values
            //return func.invoke(parameters);
            return null;
        }
    }

    /// <summary>
    /// Syntax for an invocation of a C# function.
    /// We could save this so that there isn't a search every time.
    /// </summary>
    class NativeFunctionInvocationSyntaxNode : SyntaxNode
    {
        /// <summary>
        /// MethodInfo object pointing to the C# function.
        /// </summary>
        public MethodInfo MethodInfo;

        public override object GetValue()
        {
            return null;
        }
    }

    /// <summary>
    /// Cache node for native invocations?
    /// </summary>
    class NativeMemberInvocationSyntaxNode : SyntaxNode
    {
        public MemberInfo info;

        public override object GetValue()
        {
            if (info is PropertyInfo)
            {

            }
            if (info is FieldInfo)
            {

            }
            if (info is MethodInfo)
            {

            }

            return null;
        }
    }
}
