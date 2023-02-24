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

namespace SiCode.IDE
{
    /// <summary>
    /// Logique d'interaction pour DownloadComponents.xaml
    /// </summary>
    public partial class DownloadComponents : Wpf.Ui.Controls.Window.FluentWindow
    {
        public DownloadComponents()
        {
            InitializeComponent();

            this.WindowBackdropType = (Wpf.Ui.Controls.Window.WindowBackdropType)Configuration.VisualEffect;

            archComboBox.ItemsSource = Enum.GetValues(typeof(Architecture));
            archComboBox.SelectedIndex = 0;

            componentComboBox.ItemsSource = Enum.GetValues(typeof(OptionalComponent));
            componentComboBox.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OptionalComponentManager.Download((OptionalComponent)componentComboBox.SelectedIndex, (Architecture)archComboBox.SelectedIndex);
        }
    }
}
