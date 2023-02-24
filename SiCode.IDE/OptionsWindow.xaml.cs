using SiCodeIDE;
using System.Windows;

namespace SiCode.IDE
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Wpf.Ui.Controls.Window.FluentWindow
    {
        public OptionsWindow()
        {
            InitializeComponent();

            this.WindowBackdropType = (Wpf.Ui.Controls.Window.WindowBackdropType)Configuration.VisualEffect;
        }
    }
}