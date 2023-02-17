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
using Microsoft.Win32;
using SiCodeIDE;

namespace SiCode.IDE.Dialogs
{
    /// <summary>
    /// Interaction logic for NewProjectDialog.xaml
    /// </summary>
    public partial class NewProjectDialog : Wpf.Ui.Controls.Window.FluentWindow
    {
        public bool CancelProjectCreation = false;
        public NewProjectDialog()
        {
            InitializeComponent();
            this.WindowBackdropType = (Wpf.Ui.Controls.Window.WindowBackdropType)Configuration.VisualEffect;
        }

        private void folderBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.pjLocTb.Text = fbd.SelectedPath;
            }
        }

        private void TitleBar_CloseClicked(object sender, RoutedEventArgs e)
        {
            CancelProjectCreation = true;
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(pjNameTb.Text) || String.IsNullOrEmpty(pjLocTb.Text))
            {
                MessageBox.Show("Please complete all the fields.", "SiCode IDE", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            this.Close();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.CancelProjectCreation = true;
            this.Close();
        }

    }
}
