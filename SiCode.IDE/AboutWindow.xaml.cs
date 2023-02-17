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
using SiCodeIDE;
namespace SiCode.IDE
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Wpf.Ui.Controls.Window.FluentWindow
    {
        public AboutWindow()
        {
            InitializeComponent();
            if (Configuration.Theme == 2 /**light theme**/)
            {
                label.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                label1.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                label2.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                label3.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                label4.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }

            this.WindowBackdropType = (Wpf.Ui.Controls.Window.WindowBackdropType)Configuration.VisualEffect;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
