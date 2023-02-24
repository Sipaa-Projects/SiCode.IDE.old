using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using SiCodeIDE;
using Wpf.Ui.Appearance;
using Wpf.Ui.Markup;
using SiCodeIDE.ProjectSystem;
namespace SiCode.IDE
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                // WPF default app startup routines
                base.OnStartup(e);

                // Show startup screen
                Startup s = new Startup();
                s.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                s.Show();

                // Load SiCode IDE configuration

                if (!Directory.Exists(Configuration.ConfigDir))
                {
                    Directory.CreateDirectory(Configuration.ConfigDir);
                }
                if (!Directory.Exists(Configuration.RecentProjectsDir))
                {
                    Directory.CreateDirectory(Configuration.RecentProjectsDir);
                }

                Configuration.LoadConfig();

                // Set theme
                SetTheme((ThemeType)Configuration.Theme);

                // Wait
                Thread.Sleep(1500);

                // Close startup screen & show main window
                s.Hide();

                if (!Configuration.FirstLaunch)
                {
                    if (Environment.GetCommandLineArgs().Count() != 1)
                    {
                        SiCode.IDE.MainWindow w = new SiCode.IDE.MainWindow(ProjectIniReader.OpenProject(Environment.GetCommandLineArgs()[0]));
                        w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        w.Show();
                    }
                    else
                    {
                        SiCode.IDE.HomeWindow w = new SiCode.IDE.HomeWindow();
                        w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        w.Show();
                    }
                }
                else
                {
                    SiCode.IDE.SetupWelcome w = new SiCode.IDE.SetupWelcome();
                    w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    w.Show();
                }

            }catch (Exception ex)
            {
                MessageBox.Show("A component of SiCode ran into a problem and SiCode needs to restart.\nHere's the stack trace :\n" + ex, "SiCode IDE", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        public void SetTheme(ThemeType t)
        {
            ((ThemesDictionary)Resources.MergedDictionaries[0]).Theme = t;
        }
    }
}
