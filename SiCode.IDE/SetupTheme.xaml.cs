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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace SiCode.IDE
{
    /// <summary>
    /// Logique d'interaction pour SetupTheme.xaml
    /// </summary>
    public partial class SetupTheme : Wpf.Ui.Controls.Window.FluentWindow
    {
        public SetupTheme()
        {
            InitializeComponent();
        }

        private void TitleBar_CloseClicked(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Configuration.Theme = (int)Wpf.Ui.Appearance.ThemeType.Light;
            Configuration.SaveConfig();
            SiCode.IDE.SetupFinish w = new SiCode.IDE.SetupFinish();
            w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            w.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Configuration.Theme = (int)Wpf.Ui.Appearance.ThemeType.Dark;
            Configuration.SaveConfig();
            SiCode.IDE.SetupFinish w = new SiCode.IDE.SetupFinish();
            w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            w.Show();
            this.Close();
        }
    }
}
