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
using System.Windows.Shapes;

namespace SiCode.IDE.OptionsPages
{
    /// <summary>
    /// Logique d'interaction pour BehaviorPage.xaml
    /// </summary>
    public partial class BehaviorPage : Page
    {
        public BehaviorPage()
        {
            InitializeComponent();

            tb1.IsChecked = Configuration.UseUwpNotifications;
            tb1.Checked += Tb1_Checked;
            if (Configuration.Theme == 1)
            {
                tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                tb1.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            }
        }

        private void Tb1_Checked(object sender, RoutedEventArgs e)
        {
            Configuration.UseUwpNotifications = (bool)tb1.IsChecked;
            Configuration.SaveConfig();
        }
    }
}
