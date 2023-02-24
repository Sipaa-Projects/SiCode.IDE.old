using SiCodeIDE.ProjectSystem;
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
    /// Logique d'interaction pour SetupFinish.xaml
    /// </summary>
    public partial class SetupFinish : Wpf.Ui.Controls.Window.FluentWindow
    {
        public SetupFinish()
        {
            InitializeComponent();
        }

        private void TitleBar_CloseClicked(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SiCode.IDE.HomeWindow w = new SiCode.IDE.HomeWindow();
            w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            w.Show();
            this.Close();
        }
    }
}
