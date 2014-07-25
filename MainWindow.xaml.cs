using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace RacketSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
                HistoryBox.AppendText("Testing this textbox\r\n");
            AppendStrings(Utils.GetCSharpStrings("hello_racket-test"));

            
            TreeViewItem it = new TreeViewItem();
            it.Items.Add("This is an item.");
            it.Header = "This is it's header";
            it.Items.Add("Yep, this is named \"it\" so \"it's\" legit!");
            var under = new TreeViewItem();
            under.Items.Add("Such under");
            under.Header = "Under";
            ObjectBrowserTree.Items.Add("Test!");
            it.Items.Add(under);
            ObjectBrowserTree.Items.Add(it);
            ObjectBrowserTree.Name = "Test";

            AddNodeToDisplay(new LiteralSyntaxNode("foo", 2));
            AddNodeToDisplay(new LiteralSyntaxNode("bar", "hello world"));

            var text = RunTime.GetVariableValue("string-format", new object[] { "hello, {0}", "world" });
        }

        void AddNodeToDisplay(SyntaxNode node)
        {
            object item = null;
            if (node is LiteralSyntaxNode)
            {
                var val = ((LiteralSyntaxNode)node).Value;

                item = "literal " + val.GetType().Name + ": " + val.ToString();
            }
            else if (node is FunctionInvocationSyntaxNode)
            {

            }

            if (item == null) return;

            ObjectBrowserTree.Items.Add(item);
        }

        void AppendStrings(string[] input) 
        {
            foreach (var text in input) InputBox.AppendText(text + ", ");
        }
    }
}
