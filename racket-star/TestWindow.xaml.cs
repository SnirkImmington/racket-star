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
using System.Windows.Shapes;

namespace RacketStar
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        private void TabItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var tabs = ((TabItem)sender).Parent as TabControl;
            if (tabs == null) return;

            var newTab = new TabItem();
            newTab.Style = (Style)FindResource("NiceTab");
            newTab.Header = "New Tab";
            tabs.Items.Add(newTab);
        }
    }
}
