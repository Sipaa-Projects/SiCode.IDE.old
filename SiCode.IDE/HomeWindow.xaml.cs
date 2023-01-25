using Microsoft.Win32;
using SiCode.IDE.Dialogs;
using SiCodeIDE;
using SiCodeIDE.ProjectSystem;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Wpf.Ui.Controls.Window.FluentWindow
    {
        public HomeWindow()
        {
            InitializeComponent();
            this.WindowBackdropType = (Wpf.Ui.Controls.Window.WindowBackdropType)Configuration.VisualEffect;

            listBox.ItemsSource = null;

            // Gathering recent projects
            foreach (string f in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $@"\SiCodeIDE\RecentProjects\"))
            {
                FileInfo f2 = new FileInfo(f);
                var btn = new Button() { Content = f2.Name };
                btn.Click += (s, e) =>
                {
                    MainWindow w = new MainWindow(ProjectIniReader.OpenProject(File.ReadAllText(f)));
                    w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    w.Show();
                    this.Close();
                };
                listBox.Items.Add(btn);
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            w.Show();
            this.Close();
        }

        private void newProjectButton_Click(object sender, RoutedEventArgs e)
        {
            NewProjectDialog w = new NewProjectDialog();
            w.Show();
            w.Closing += (s, ev) =>
            {
                if (!w.CancelProjectCreation)
                {
                    ProjectIniReader.CreateProject(w.pjNameTb.Text, (ProjectType)w.pjTypeCb.SelectedIndex, w.pjLocTb.Text);
                    MainWindow w2 = new MainWindow(ProjectIniReader.OpenProject(w.pjLocTb.Text + @"\" + w.pjNameTb.Text + ".ini"));
                    w2.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    w2.Show();
                    this.Close();
                }
            };
        }

        private void openProjectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SiCode IDE Project files|*.ini";
            if (ofd.ShowDialog() == true)
            {
                MainWindow w = new MainWindow(ProjectIniReader.OpenProject(ofd.FileName));
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.Show();
                this.Close();
            }
        }

        private void TitleBar_CloseClicked(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
