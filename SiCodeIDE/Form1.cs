using SiCodeIDE.Dialogs;
using SiCodeIDE.ProjectSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiCodeIDE
{
    public partial class Form1 : Form
    {
        Project CurrentProject;
        CsCompiler csc; 
        VbCompiler vbc;
        public Form1()
        {
            InitializeComponent();
            csc = new CsCompiler();
            vbc = new VbCompiler();
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

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "SiCode IDE project file|*.ini";
            if (f.ShowDialog() == DialogResult.OK)
            {
                var proj = ProjectIniReader.OpenProject(f.FileName);
                if (proj.Name != "Unknown")
                {
                    CurrentProject = proj;
                    Text = "SiCode IDE - " + proj.Name;
                    fastColoredTextBox1.OpenFile(proj.Program);
                    if (CurrentProject.ProjectType == ProjectType.Website)
                    {
                        fastColoredTextBox1.Language = FastColoredTextBoxNS.Language.HTML; fastColoredTextBox1.Invalidate(); button2.Visible = false;
                    } 
                    else if (CurrentProject.ProjectType == ProjectType.CSharpApplication || CurrentProject.ProjectType == ProjectType.CSharpLibrary)
                    { 
                        button2.Visible = true; fastColoredTextBox1.Language = FastColoredTextBoxNS.Language.CSharp; fastColoredTextBox1.Invalidate(); button2.Visible = true;
                    }
                    else if (CurrentProject.ProjectType == ProjectType.VisualBasicApplication || CurrentProject.ProjectType == ProjectType.VisualBasicLibrary)
                    { 
                        button2.Visible = true; fastColoredTextBox1.Language = FastColoredTextBoxNS.Language.VB; fastColoredTextBox1.Invalidate(); button2.Visible = true;
                    }
                }
            }
        }

        private void crToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProjectDialog d = new NewProjectDialog();
            d.ProjectCreated += (s,ee) =>
            {
                CurrentProject = d.CreatedProject;
                Text = "SiCode IDE - " + CurrentProject.Name;
                fastColoredTextBox1.OpenFile(CurrentProject.Program);
                if (CurrentProject.ProjectType == ProjectType.Website)
                {
                    fastColoredTextBox1.Language = FastColoredTextBoxNS.Language.HTML; fastColoredTextBox1.Invalidate(); button2.Visible = false;
                }
                else if (CurrentProject.ProjectType == ProjectType.CSharpApplication || CurrentProject.ProjectType == ProjectType.CSharpLibrary)
                {
                    button2.Visible = true; fastColoredTextBox1.Language = FastColoredTextBoxNS.Language.CSharp; fastColoredTextBox1.Invalidate(); button2.Visible = true;
                }
                else if (CurrentProject.ProjectType == ProjectType.VisualBasicApplication || CurrentProject.ProjectType == ProjectType.VisualBasicLibrary)
                {
                    button2.Visible = true; fastColoredTextBox1.Language = FastColoredTextBoxNS.Language.VB; fastColoredTextBox1.Invalidate(); button2.Visible = true;
                }
                d.Close();
            };

            d.Closed += (s, ee) =>
            {
                d.Dispose();
            };

            d.Show();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(CurrentProject.Program))
            {
                fastColoredTextBox1.SaveToFile(CurrentProject.Program, Encoding.UTF8);
            }
            else
            {
                MessageBox.Show("Please open a project before saving the program", "SiCode IDE");
            }
        }

        private void aboutSiCodeIDEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutDialog().Show();
        }

        private void heloToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(CurrentProject.ProjectDir))
            {
                MessageBox.Show("Please open a project before compiling", "SiCode IDE");
            }
            else
            {
                fastColoredTextBox1.SaveToFile(CurrentProject.Program, Encoding.UTF8);
                if (CurrentProject.ProjectType == ProjectType.CSharpApplication || CurrentProject.ProjectType == ProjectType.CSharpLibrary)
                    csc.Compile(CurrentProject);
                else
                    vbc.Compile(CurrentProject);
            }
        }

        private void seeLastCompileErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentProject.ProjectType == ProjectType.CSharpApplication || CurrentProject.ProjectType == ProjectType.CSharpLibrary)
                new CompileLogsDialog(csc.LastCompileErrors).Show();
            else if (CurrentProject.ProjectType == ProjectType.VisualBasicApplication || CurrentProject.ProjectType == ProjectType.VisualBasicLibrary)
                new CompileLogsDialog(vbc.LastCompileErrors).Show();
        }

        private void seeLastCompilerLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentProject.ProjectType == ProjectType.CSharpApplication || CurrentProject.ProjectType == ProjectType.CSharpLibrary)
                new CompileLogsDialog(csc.LastCompileLogs).Show();
            else if (CurrentProject.ProjectType == ProjectType.VisualBasicApplication || CurrentProject.ProjectType == ProjectType.VisualBasicLibrary)
                new CompileLogsDialog(vbc.LastCompileLogs).Show();
        }

        private void runCompiledApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(CurrentProject.ProjectDir))
            {
                MessageBox.Show("Please open a project before run the compiled executable", "SiCode IDE");
            }
            else
            {
                if (File.Exists(CurrentProject.ProjectDir + @"\Binary\" + CurrentProject.Name + ".exe"))
                {
                    Process.Start(CurrentProject.ProjectDir + @"\Binary\" + CurrentProject.Name + ".exe");
                }
                else
                {
                    MessageBox.Show("Cannot find compiled executable! Maybe you compiled a library, or you forgot to compile your application?", "SiCode IDE");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileContextMenu.Show((Button)sender, 0, ((Button)sender).Height);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CompilerContextMenu.Show((Button)sender, 0, ((Button)sender).Height);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HelpContextMenu.Show((Button)sender, 0, ((Button)sender).Height);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OptionsContextMenu.Show((Button)sender, 0, ((Button)sender).Height);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new SiCodeProperties().Show();
        }

        private void fctbTextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(CurrentProject.Program) && Configuration.EnableAutoSave)
            {
                fastColoredTextBox1.SaveToFile(CurrentProject.Program, Encoding.UTF8);
            }
        }

        private void showInFileExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(CurrentProject.ProjectDir))
                Process.Start("explorer.exe", CurrentProject.ProjectDir);
            else
                MessageBox.Show("Please open a project before using this option", "SiCode IDE");
        }

        private void showInCommandPromptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(CurrentProject.ProjectDir))
                Process.Start("C:\\Windows\\System32\\cmd.exe", "/K cd /d " + CurrentProject.ProjectDir);
            else
                MessageBox.Show("Please open a project before use this option", "SiCode IDE");
        }

        private void fastColoredTextBox1_Load(object sender, EventArgs e)
        {

        }

        private void referencedAssembliesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(CurrentProject.Name))
            {
                new Dialogs.ReferencedAssemblies(CurrentProject).Show();
            }
            else
            {
                MessageBox.Show("Please open a project before use this option", "SiCode IDE");
            }
        }
    }
}
