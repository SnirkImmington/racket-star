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

namespace RacketStar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Set the title
            Title = "Untitled - " + Utils.Lambda + Utils.Star;
            // Create click button
            var button = GUI.GUItils.GetLinkButton(this, "Click here!");
            WriteControl(button);
            LastEdited = HistoryBox.Document.ContentEnd;
        }

        void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked the link!!1!!!");
        }

        #region Controls/Utils

        /// <summary>
        /// Add a WPF control to the history box.
        /// </summary>
        /// <param name="trol">The control to add.</param>
        public void WriteControl(Control trol)
        {
            // If the last block is a stackpanel, add it to the children.
            var lastBlock = HistoryBox.Document.Blocks.LastBlock;
            if (lastBlock is BlockUIContainer)
            {
                var container = ((BlockUIContainer)lastBlock).Child as StackPanel;
                container.Children.Add(trol);
            }
            else // We need to add a new block for them.
            {
                var panel = GUI.GUItils.GetDocumentStackPanel();
                panel.Children.Add(trol);
                HistoryBox.Document.Blocks.Add(new BlockUIContainer(panel));
            }
            HistoryBox.CaretPosition = HistoryBox.Document.ContentEnd;
        }

        /// <summary>
        /// Add a label control to the history box.
        /// </summary>
        /// <param name="text">The text of the label control.</param>
        public void WriteLabel(string text)
        {
            var label = new Label();
            label.Content = text;
            WriteControl(label);
        }

        public void SetLanguage(string languageName)
        {
            LanguageLabel.Content = languageName;
        }



        #endregion
    }

    public class TabInfo
    {
        public TextPointer LastEditable;
    }
}
