using SiCodeIDE;
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
using Wpf.Ui.Appearance;

namespace SiCode.IDE.OptionsPages
{
    /// <summary>
    /// Logique d'interaction pour AppearancePage.xaml
    /// </summary>
    public partial class TextEditorPage : Page
    {
        public TextEditorPage()
        {
            InitializeComponent();

            tb1.IsChecked = Configuration.EnableAutoSave;
            tb1.Checked += Tb1_Checked;

            if (Configuration.Theme == 1)
            {
                tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                tb1.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            }
        }

        private void Tb1_Checked(object sender, RoutedEventArgs e)
        {
            Configuration.EnableAutoSave = (bool)tb1.IsChecked;
            Configuration.SaveConfig();
        }
    }
}
