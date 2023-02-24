
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

namespace SiCode.IDE
{
    /// <summary>
    /// Logique d'interaction pour AskChatGPT.xaml
    /// </summary>
    public partial class AskChatGPT : Window
    {
        public AskChatGPT()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // yes i don't really care of my openai api key...
            //var api = new OpenAIAPI("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");
            //var result = api.Completions.GetCompletion("One Two Three One Two");
            //Console.WriteLine(result);
            
        }
    }
}
