using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SiCodeIDE.ProjectSystem;

namespace SiCodeIDE.Dialogs
{
    public partial class ReferencedAssemblies : Form
    {
        Project p;

        internal ReferencedAssemblies(Project p)
        {
            InitializeComponent();
            this.p = p;
            foreach (string ra in p.ReferencedAssemblies.GetReferencedAssemblies()) 
            {
                listBox1.Items.Add(ra);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedAssembly = (string)listBox1.SelectedItem;
            FileInfo f = new FileInfo(selectedAssembly);
            File.Delete(p.ProjectDir + @"\Binary\" + f.Name);
            listBox1.Items.Remove(selectedAssembly);
            p.ReferencedAssemblies.RemoveReferencedAssembly(selectedAssembly);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ".NET Library|*.dll";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileInfo f = new FileInfo(ofd.FileName);
                listBox1.Items.Add(ofd.FileName);
                p.ReferencedAssemblies.AddReferencedAssembly(ofd.FileName);
                if (!Directory.Exists(p.ProjectDir + @"\Binary\"))
                {
                    Directory.CreateDirectory(p.ProjectDir + @"\Binary\");
                }
                File.Copy(ofd.FileName, p.ProjectDir + @"\Binary\" + f.Name, true);
            }
        }

        private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            p.ReferencedAssemblies.SaveReferencedAssemblies();
        }
    }
}
