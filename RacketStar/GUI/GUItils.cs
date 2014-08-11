using RacketStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace RacketStar.GUI
{
    /// <summary>
    /// Contains utils and factory methods for making GUI elements.
    /// </summary>
    static class GUItils
    {
        /// <summary>
        /// Gets a TextBlock with highlighted and underlined text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static TextBlock GetHyperlinkBlock(string text)
        {
            // Create TextBlock with text on it, underlined.
            var turn = new TextBlock(new Underline(new Run(text)));
            // Set the font family to Consolas
            turn.FontFamily = new FontFamily("Consolas");
            // Make the foreground blue
            turn.Foreground = new SolidColorBrush(Colors.Blue);
            return turn;
        }

        /// <summary>
        /// Creates a button to be put into the flow document that looks like a link
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Button GetLinkButton(MainWindow window, string text)
        {
            // Make a button.
            var turn = new Button();
            // Set its style to the proper style.
            turn.Style = (Style)window.FindResource("DocumentButton");
            // Just disable the border brush.
            turn.BorderBrush = null;
            turn.HorizontalAlignment = HorizontalAlignment.Left;
            // Set its content
            turn.Content = GetHyperlinkBlock(text);
            return turn;
        }
    
        /// <summary>
        /// Creates a properly lined stackpanel for use in flowdocuments.
        /// </summary>
        public static StackPanel GetDocumentStackPanel()
        {
            var turn = new StackPanel();
            // Set the orientation properly
            turn.Orientation = Orientation.Horizontal;
            // Make the controls look nice
            turn.Margin = new Thickness(0);
            turn.HorizontalAlignment = HorizontalAlignment.Left;
            return turn;
        }
    }
}
