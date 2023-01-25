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
using SiCodeIDE;

namespace SiCode.IDE
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Wpf.Ui.Controls.Window.FluentWindow
    {
        public OptionsWindow()
        {
            InitializeComponent();

            comboBox.ItemsSource = Enum.GetValues(typeof(Wpf.Ui.Controls.Window.WindowBackdropType));
            comboBox.SelectedIndex = Configuration.VisualEffect;

            this.WindowBackdropType = (Wpf.Ui.Controls.Window.WindowBackdropType)Configuration.VisualEffect;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Configuration.VisualEffect = comboBox.SelectedIndex;
            Configuration.EnableAutoSave = (bool)enableAsCheckBox.IsChecked;

            Configuration.SaveConfig();

            Wpf.Ui.Controls.MessageBox msgBox = new Wpf.Ui.Controls.MessageBox();
            msgBox.Title = "SiCode IDE";
            msgBox.Content = "You need to restart SiCode IDE to apply the changes.";
            msgBox.ButtonRightName = "OK";
            msgBox.ButtonLeftAppearance = Wpf.Ui.Controls.ControlAppearance.Transparent;
            msgBox.Show();
        }
    }
}
