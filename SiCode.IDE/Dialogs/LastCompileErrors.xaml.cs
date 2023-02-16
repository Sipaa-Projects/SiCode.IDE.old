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
using SiCodeIDE;
namespace SiCode.IDE.Dialogs
{
    /// <summary>
    /// Interaction logic for LastCompileErrors.xaml
    /// </summary>
    public partial class LastCompileErrors : Wpf.Ui.Controls.Window.FluentWindow
    {
        public LastCompileErrors(string compileErrors)
        {
            InitializeComponent();
            this.compileErrors.Text = compileErrors;
            this.WindowBackdropType = (Wpf.Ui.Controls.Window.WindowBackdropType)Configuration.VisualEffect;
        }
    }
}
