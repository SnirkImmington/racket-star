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
using System.Reflection;
using System.Diagnostics;
using RacketStar.Runtime;

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

            /*
            // Load old content
            if (Properties.Settings.Default.Editor_LastFiles.Count != 0)
            {
                
            }

            // Fill the menu
            if (Properties.Settings.Default.Editor_RecentFiles.Count != 0)
            {
                var recentMenu = new MenuItem();
                recentMenu.Header = "_Recent";
                foreach (var file in Properties.Settings.Default.Editor_RecentFiles)
                {
                    recentMenu.Items.Add(file);
                }
                FileMenu.Items.Add(new Separator());
                FileMenu.Items.Add(recentMenu);
            }
            */

            // Apply settings to textboxes
            //HistoryBox.FontFamily = new FontFamily(Properties.Settings.Default.GUI_FontFamily);

            // Events
            HistoryBox.KeyDown += HistoryBox_KeyDown;
            HistoryBox.SelectionChanged += HistoryBox_SelectionChanged;

            // Create click button
            // Clear the document
            HistoryBox.Document = new FlowDocument();
            HistoryBox.AppendText("{0}{1} version {2} initialized. ".Format(Utils.Lambda, Utils.Star, Assembly.GetExecutingAssembly().GetName().Version));
            WriteHyperLink("Click here", new Uri("http://racket-lang.org/"));
            HistoryBox.AppendText(" for Racket documentation.");
        }

        #region Events
        
        void HistoryBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var box = sender as RichTextBox;
            if (box.Selection.IsEmpty) InputBox.Focus();
        }
        
        void HistoryBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Move the typing over to the input box
            InputBox.Focus();
            InputBox.RaiseEvent(e);
        }

        #endregion

        #region Controls

        #endregion

        #region Control Methods

        /// <summary>
        /// Writes a HyperLink inline to the HistoryBox.
        /// </summary>
        /// <param name="text">Text of the link</param>
        /// <param name="uri">URI to navigate to</param>
        public void WriteHyperLink(string text, Uri uri = null)
        {
            var link = new Hyperlink(new Run(text));
            link.NavigateUri = uri;
            link.ToolTip = uri.ToString();
            link.IsEnabled = true;
            AppendInline(link);
        }

        /// <summary>
        /// Add a WPF control to the history box.
        /// </summary>
        /// <param name="trol">The control to add.</param>
        public void WriteControl(Control trol)
        {
            AppendInline(new InlineUIContainer(trol));
        }

        /// <summary>
        /// Appends the inline control to HistoryBox in an inline fashion.
        /// </summary>
        /// <param name="inline"></param>
        public void AppendInline(Inline inline)
        {
            var block = HistoryBox.Document.Blocks.LastBlock;
            if (block == null || !(block is Paragraph))
            {
                block = new Paragraph();
                HistoryBox.Document.Blocks.Add(block);
            }
            var para = block as Paragraph;
            para.Inlines.Add(inline);
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

        private void OnClosed(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }

    public class TabInfo
    {
        public TextPointer LastEditable;
    }
}
