using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RacketStar.Workspace;

namespace RacketStar
{
    /// <summary>
    /// Basic SyntaxNode for just some text, created before the arguments are understood
    /// </summary>
    class CompilationTextSyntax : SyntaxNode
    {
        /// <summary>
        /// The text in the code
        /// </summary>
        public string Text;

        /// <summary>
        /// Returns the text.
        /// </summary>
        public override object GetValue(bool runTime, LanguageDialect dialect)
        {
            return Text;
        }

        /// <summary>
        /// Constructor with text.
        /// </summary>
        public CompilationTextSyntax(string text)
        {
            Text = text;
        }
    }

    /// <summary>
    /// Syntax for comments.
    /// </summary>
    class CommentSyntax : SyntaxNode
    {
        /// <summary>
        /// The comment.
        /// </summary>
        public string Comment;

        /// <summary>
        /// Returns the comment.
        /// </summary>
        public override object GetValue(bool runTime, LanguageDialect dialect)
        {
            return Comment;
        }

        /// <summary>
        /// Constructor with text.
        /// </summary>
        public CommentSyntax(string comment)
        {
            Comment = comment;
        }
    }

    /// <summary>
    /// Syntax node for documentation artifacts.
    /// </summary>
    class DocumentationSyntax : SyntaxNode
    {
        /// <summary>
        /// Could be a MethodDocumentation or just a basic documentation
        /// </summary>
        public BasicDocumentation Documentation;

        /// <summary>
        /// Returns the documentation object.
        /// </summary>
        public override object GetValue(bool runTime, LanguageDialect dialect)
        {
            return Documentation;
        }

        /// <summary>
        /// Constructor for Documentation
        /// </summary>
        public DocumentationSyntax(BasicDocumentation document)
        {
            Documentation = document;
        }
    }
}
