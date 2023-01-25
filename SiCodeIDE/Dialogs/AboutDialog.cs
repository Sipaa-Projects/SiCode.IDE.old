using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiCodeIDE.Dialogs
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/RaphMar2021/SiCodeIDE");
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
    }
}
