using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace racket_sharp
{    
    /// <summary>
    /// Syntax for type arguments, usually denoted with a dash
    /// (define ints (list-new -int))
    /// </summary>
    class TypeArgumentSyntax : SyntaxNode
    {
        /// <summary>
        /// The type specified in the argument.
        /// </summary>
        public Type Type;

        /// <summary>
        /// Returns this.Type.
        /// </summary>
        public override object GetValue()
        {
            return Type;
        }

        /// <summary>
        /// Constructor with type.
        /// </summary>
        public TypeArgumentSyntax(Type type)
        {
            Type = type;
        }
    }

    #region Type Creation

    /// <summary>
    /// Syntax for declaring a class:
    /// (class list -t
    ///     (field x int)
    ///     (field c t)
    /// )
    /// </summary>
    class SharpClassDeclarationSyntax : SyntaxNode
    {
        /// <summary>
        /// Type arguments to the class.
        /// </summary>
        public TypeArgumentDeclarationSyntax[] TypeArguments;

        public SharpConstructorDeclarationSyntax[] Constructors;

        public SharpFieldDeclarationSyntax[] Fields;

        public SharpPropertyDeclarationSyntax[] Properties;
    }

    /// <summary>
    /// Argument for declared types, such as the T in "class List&lt;T&gt; {"
    /// </summary>
    class TypeArgumentDeclarationSyntax : TypeArgumentSyntax
    {
        /// <summary>
        /// The name (such as t) of the type argument.
        /// </summary>
        public string TypeName;

        public TypeArgumentDeclarationSyntax(string typeName, Type type) : base(type)
        {
            TypeName = typeName;
        }
    }

    class SharpFieldDeclarationSyntax : SyntaxNode
    {

    }

    class SharpPropertyDeclarationSyntax : SyntaxNode
    {

    }

    class SharpConstructorDeclarationSyntax : SyntaxNode
    {

    }

    class SharpMethodDeclarationSyntax : SyntaxNode
    {
        public FunctionInvocationSyntaxNode[] Functions;
    }

    #endregion
}
