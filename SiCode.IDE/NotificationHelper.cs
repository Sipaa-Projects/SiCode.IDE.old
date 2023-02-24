using Microsoft.Toolkit.Uwp.Notifications;
using SiCodeIDE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SiCode.IDE
{
    internal class NotificationHelper
    {
        /// <summary>
        /// Send a UWP notification or a message box
        /// </summary>
        /// <param name="sender">The sender's type</param>
        /// <param name="text">The text to send</param>
        internal static void ShowNotification(Type sender, string text, MessageBoxImage img) 
        {
            string title = "";
            if (sender == null) 
            {
                title = "Unknown sender";
            }
            else
            {
                title = sender.FullName;
            }

            if (Configuration.UseUwpNotifications)
            {
                new ToastContentBuilder()
                            .AddHeader(title.Replace(' ', '_').ToLower(), title, "")
                            .AddText(text)
                            .Show();
            }
            else
            {
                MessageBox.Show(text, title, MessageBoxButton.OK, img);
            }
        }
    }
}
