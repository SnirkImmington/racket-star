using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;

namespace racket_sharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Title = "Untitled - ProfRacketSharp";

            try
            {
                //HistoryBox.AppendText( typeof(int).IsAssignableFrom(typeof(long)).ToString() );
                //HistoryBox.AppendText("(string-length \"hello, world\") = " + RunTime.GetVariableValue("string-length", new object[] { "hello, world" }).ToString());
                //TheLabel.Content = RunTime.GetVariableValue("string-format", new object[] { "hello, {0}", new object[] { "world" } }).ToString();

                var type = typeof(TestClass<foo>);
                var methodInfo = type.GetMethod("getValue");

                if (methodInfo.IsGenericMethod)
                {
                    var types = methodInfo.GetGenericArguments();
                    var genericType = types[0];
                    if (types[0].GenericParameterPosition != -1)
                    {
                        if (genericType.IsGenericParameter)
                        {

                        }
                    }
                    var length = types.Length;
                    methodInfo = methodInfo.MakeGenericMethod(new Type[] { typeof(string) });
                }
                methodInfo.Invoke(null, null);
            }
            catch (Exception ex)
            {
                HistoryBox.AppendText(ex.ToString());
            }
        }
    }

    class foo
    {
        public foo()
        {

        }
    }
}
