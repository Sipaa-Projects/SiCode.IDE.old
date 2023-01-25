using SiCodeIDE.ProjectSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiCodeIDE.Dialogs
{
    public partial class NewProjectDialog : Form
    {
        internal Project CreatedProject;
        internal event EventHandler ProjectCreated;
        internal event EventHandler Closed;
        public NewProjectDialog()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog d = new FolderBrowserDialog();
            if (d.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = d.SelectedPath;
            }
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x01 /*WM_CREATE window message*/)
            {
                int darkPref = (int)DesktopWindowManager.DWM_BOOL.DWMWCP_TRUE;
                DesktopWindowManager.DwmSetWindowAttribute(Handle, DesktopWindowManager.DwmWindowAttribute.UseImmersiveDarkMode, ref darkPref, sizeof(int));

                if (Configuration.EnableVisualEffects)
                {
                    int micaAltPref = (int)DesktopWindowManager.DWM_SYSTEMBACKDROP_TYPE.DWMSBT_TABBEDWINDOW;
                    DesktopWindowManager.MARGINS ma = new DesktopWindowManager.MARGINS() { bottomHeight = -1, leftWidth = -1, rightWidth = -1, topHeight = -1 };

                    DesktopWindowManager.DwmExtendFrameIntoClientArea(Handle, ref ma);
                    DesktopWindowManager.DwmSetWindowAttribute(Handle, DesktopWindowManager.DwmWindowAttribute.SystemBackdropType, ref micaAltPref, sizeof(int));
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ProjectIniReader.CreateProject(textBox1.Text, (ProjectType)comboBox1.SelectedIndex, textBox2.Text);
            CreatedProject = ProjectIniReader.OpenProject(textBox2.Text + @"\" + textBox1.Text + ".ini");
            if (ProjectCreated != null)
                ProjectCreated.Invoke(this, new EventArgs());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (Closed != null)
                Closed.Invoke(this, new EventArgs());
        }
    }
}
