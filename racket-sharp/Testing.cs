using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;

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

    class TestingMethods
    {
        public static void foo()
        {
            int x;
            string text = "";
            int y;
        }

        public static void GetMethodInfo(MainWindow window, MethodInfo method)
        {
            window.WriteLine(method.Name);

            var data = method.GetMethodBody();

            foreach (var variable in data.LocalVariables)
                window.WriteLine(variable.LocalIndex + ": " + variable.LocalType);
        }

        public static void GetCoolerMethodInfo(System.CodeDom.CodeMemberMethod method)
        {
            CodeCompileUnit unit = new CodeCompileUnit();
            CodeNamespace namespaceSample = new CodeNamespace("Sample");
            namespaceSample.Imports.Add(new CodeNamespaceImport("System"));
            unit.Namespaces.Add(namespaceSample);
            CodeTypeDeclaration declaration = new CodeTypeDeclaration("Class1");
            namespaceSample.Types.Add(declaration);
        }
    }
}
