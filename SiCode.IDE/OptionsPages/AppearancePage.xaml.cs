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
    public partial class AppearancePage : Page
    {
        public AppearancePage()
        {
            InitializeComponent();

            themeComboBox.ItemsSource = Enum.GetValues(typeof(Wpf.Ui.Appearance.ThemeType));
            themeComboBox.SelectedIndex = Configuration.Theme;
            themeComboBox.SelectionChanged += themeComboBox_SelectionChanged;

            backdropComboBox.ItemsSource = Enum.GetValues(typeof(Wpf.Ui.Controls.Window.WindowBackdropType));
            backdropComboBox.SelectedIndex = Configuration.VisualEffect;
            backdropComboBox.SelectionChanged += backdropComboBox_SelectionChanged;

            if (Configuration.Theme == 1)
            {
                tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                tb1.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                tb2.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            }
        }

        private void themeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Configuration.Theme = ((ComboBox)sender).SelectedIndex;
            Configuration.SaveConfig();
            themeWarningSnackbar.Show("Information", "Theme will not apply unless you restart SiCode IDE.", Wpf.Ui.Common.SymbolRegular.Info20, Wpf.Ui.Controls.ControlAppearance.Info);
        }

        private void backdropComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Configuration.VisualEffect = ((ComboBox)sender).SelectedIndex;
            Configuration.SaveConfig();
            themeWarningSnackbar.Show("Information", "Backdrop will not apply unless you reopen SiCode IDE windows.", Wpf.Ui.Common.SymbolRegular.Info20, Wpf.Ui.Controls.ControlAppearance.Info);
        }
    }
}
