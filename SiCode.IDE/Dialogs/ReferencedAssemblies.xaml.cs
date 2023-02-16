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
using Microsoft.Win32;
using SiCodeIDE.ProjectSystem;
using SiCodeIDE;

namespace SiCode.IDE.Dialogs
{
    /// <summary>
    /// Interaction logic for NewProjectDialog.xaml
    /// </summary>
    public partial class ReferencedAssemblies : Wpf.Ui.Controls.Window.FluentWindow
    {
        public Project p;

        public ReferencedAssemblies(Project p)
        {
            InitializeComponent();
            listBox.ItemsSource = p.ReferencedAssemblies.GetReferencedAssemblies();
            this.p = p;
            this.WindowBackdropType = (Wpf.Ui.Controls.Window.WindowBackdropType)Configuration.VisualEffect;
        }

        private void TitleBar_CloseClicked(object sender, RoutedEventArgs e)
        {
            p.ReferencedAssemblies.SaveReferencedAssemblies();
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            p.ReferencedAssemblies.SaveReferencedAssemblies();
            this.Close();
        }

        private void button_Copy_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ".NET Library|*.dll";
            if (ofd.ShowDialog() == true)
            {
                FileInfo f = new FileInfo(ofd.FileName);
                p.ReferencedAssemblies.AddReferencedAssembly(ofd.FileName);
                if (!Directory.Exists(p.ProjectDir + @"\Binary\"))
                {
                    Directory.CreateDirectory(p.ProjectDir + @"\Binary\");
                }
                File.Copy(ofd.FileName, p.ProjectDir + @"\Binary\" + f.Name, true);

                listBox.ItemsSource = null;
                listBox.ItemsSource = p.ReferencedAssemblies.GetReferencedAssemblies();
            }
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                var selectedAssembly = listBox.SelectedIndex;
                FileInfo f = new FileInfo(p.ReferencedAssemblies.GetReferencedAssemblies()[selectedAssembly]);
                File.Delete(p.ProjectDir + @"\Binary\" + f.Name);
                p.ReferencedAssemblies.RemoveReferencedAssembly(p.ReferencedAssemblies.GetReferencedAssemblies()[selectedAssembly]);
                listBox.ItemsSource = null;
                
                listBox.ItemsSource = p.ReferencedAssemblies.GetReferencedAssemblies();
            }
        }
    }
}
