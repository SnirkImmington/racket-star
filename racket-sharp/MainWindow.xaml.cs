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

                RunTime.AddBasicAssemblies();

                var msCoreLib = RunTime.ReferencedAssemblies[0];

                var listType = typeof(List<>);
                var name = listType.FullName;
                var assemName = listType.AssemblyQualifiedName;

                WriteLine(name + ", " + assemName);

                var type = RunTime.GetTypeFromAssembly(msCoreLib, "List", true);

                var list = (List<int>)RunTime.GetVariableValue("list-new", new object[] { }, new Type[] { typeof(int) });

                list.AddRange(new int[] { 1, 2, 3, 4, 5 });

                WriteLine(RunTime.GetVariableValue("list-get", new object[] { list, 3}, new Type[] {}).ToString());
            }
            catch (Exception ex)
            {
                HistoryBox.AppendText(ex.ToString());
            }
        }

        public void WriteLine(string message)
        {
            HistoryBox.AppendText(message + "\n");
        }
    }

    class foo
    {
        public foo()
        {

        }
    }
}
