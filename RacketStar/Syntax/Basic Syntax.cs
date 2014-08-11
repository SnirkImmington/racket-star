using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using RacketStar.Runtime;

namespace RacketStar
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
        public abstract object GetValue(bool runTime, LanguageDialect dialect);

        /// <summary>
        /// Gets the objects value with a specific type, throws an exception if it cannot be obtained.
        /// </summary>
        /// <typeparam name="T">Type of value to get</typeparam>
        /// <returns>A casted object of type T</returns>
        /// <exception cref="InvalidSyntaxException">
        /// If the value is not actually a T
        /// </exception>
        public T GetValue<T>(bool runTime, LanguageDialect dialect)
        {
            var value = GetValue(runTime, dialect);

            if (value is T) return (T)value;

            throw new InvalidSyntaxException("GetValue was expected to return a " + typeof(T).ToString() + ", returned a " + value.GetType().ToString() + ".");
        }
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

        ///<summary>
        /// We will compile "strings", 'strings', chars ('c'),
        /// numbers as ints, floats or doubles for C# interopability
        /// </summary>
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
        public override object GetValue(bool runTime, LanguageDialect dialect)
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

        // Depending on the dialect these could be different classes.
        public SyntaxNode[] Parameters;

        public FunctionInvocationSyntaxNode(string functionName, SyntaxNode[] parameters)
        {
            Name = functionName; Parameters = parameters;
        }

        public override object GetValue(bool runTime, LanguageDialect dialect)
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

        public override object GetValue(bool runTime, LanguageDialect dialect)
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

        public override object GetValue(bool runTime, LanguageDialect dialect)
        {
            if (info is PropertyInfo)
            {

            }
            if (info is FieldInfo)
            {

            }

            return null;
        }
    }
}
