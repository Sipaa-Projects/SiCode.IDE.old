
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
using CefSharp;
using SiCodeIDE;

namespace SiCode.IDE
{
    /// <summary>
    /// Logique d'interaction pour AskChatGPT.xaml
    /// </summary>
    public partial class AskChatGPT : Wpf.Ui.Controls.Window.FluentWindow
    {
        public AskChatGPT()
        {
            InitializeComponent();
            cwb.Address = "https://chat.openai.com";

            this.WindowBackdropType = (Wpf.Ui.Controls.Window.WindowBackdropType)Configuration.VisualEffect;
        }
    }
}
