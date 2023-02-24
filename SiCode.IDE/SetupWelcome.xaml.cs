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
    /// Logique d'interaction pour SetupWelcome.xaml
    /// </summary>
    public partial class SetupWelcome : Wpf.Ui.Controls.Window.FluentWindow
    {
        public SetupWelcome()
        {
            InitializeComponent();
        }

        private void TitleBar_CloseClicked(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SiCode.IDE.SetupTheme w = new SiCode.IDE.SetupTheme();
            w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            w.Show();
            this.Close();
        }
    }
}
