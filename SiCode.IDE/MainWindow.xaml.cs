using ICSharpCode.AvalonEdit.Folding;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Win32;
using System.IO;
using SiCodeIDE.ProjectSystem;
using SiCode.IDE.Dialogs;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using SiCodeIDE;
using System.Diagnostics;
using System.Xml;
using Wpf.Ui.Appearance;
using Microsoft.Toolkit.Uwp.Notifications;
using Wpf.Ui.Controls;
//using Wpf.Ui.Controls;

namespace SiCode.IDE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Wpf.Ui.Controls.Window.FluentWindow
    {
        VbCompiler vbc;
        CsCompiler csc;


        Project CurrentProject;

        FoldingManager foldingManager;
        BaseFoldingStrategy foldingStrategy;
        DispatcherTimer foldingUpdateTimer;

        public MainWindow()
        {
            InitializeComponent();
            this.Background = null;
            foldingManager = FoldingManager.Install(textEditor.TextArea);
            foldingStrategy = new BraceFoldingStrategy();

            vbc = new VbCompiler();
            csc = new CsCompiler();

            foldingUpdateTimer = new DispatcherTimer();
            foldingUpdateTimer.Interval = TimeSpan.FromSeconds(0.01);
            foldingUpdateTimer.Tick += delegate
            {
                foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
            };
            foldingUpdateTimer.Start();

            this.WindowBackdropType = (Wpf.Ui.Controls.Window.WindowBackdropType)Configuration.VisualEffect;
            this.textEditor.TextChanged += TextEditor_TextChanged;

            if (Configuration.Theme == (int)ThemeType.Dark)
                textEditor.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        public MainWindow(Project p)
        {
            InitializeComponent();
            this.Background = null;
            foldingManager = FoldingManager.Install(textEditor.TextArea);
            foldingStrategy = new BraceFoldingStrategy();

            vbc = new VbCompiler();
            csc = new CsCompiler();

            foldingUpdateTimer = new DispatcherTimer();
            foldingUpdateTimer.Interval = TimeSpan.FromSeconds(0.01);
            foldingUpdateTimer.Tick += delegate
            {
                foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
            };
            foldingUpdateTimer.Start();

            this.WindowBackdropType = (Wpf.Ui.Controls.Window.WindowBackdropType)Configuration.VisualEffect;
            this.textEditor.TextChanged += TextEditor_TextChanged;

            CurrentProject = p;

            if (!CurrentProject.Equals(ProjectIniReader.UnknownProject))
            {
                titleTextBlock.Text = "SiCode IDE - " + CurrentProject.Name;

                switch ((int)CurrentProject.ProjectType)
                {
                    case 0:
                        textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(textEditor.Options);
                        foldingUpdateTimer.Stop();
                        foldingStrategy = new BraceFoldingStrategy();
                        foldingUpdateTimer.Tick += delegate
                        {
                            foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
                        };
                        foldingUpdateTimer.Start();
                        textEditor.SyntaxHighlighting = HighlightingLoader.Load(HighlightingLoader.LoadXshd(new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "HighlightRefs\\CS.xshd")), HighlightingManager.Instance);
                        break;
                    case 1:
                        textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(textEditor.Options);
                        foldingUpdateTimer.Stop();
                        foldingStrategy = new BraceFoldingStrategy();
                        foldingUpdateTimer.Tick += delegate
                        {
                            foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
                        };
                        foldingUpdateTimer.Start();
                        textEditor.SyntaxHighlighting = HighlightingLoader.Load(HighlightingLoader.LoadXshd(new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "HighlightRefs\\CS.xshd")), HighlightingManager.Instance);
                        break;
                    case 2:
                        foldingUpdateTimer.Stop();
                        foldingStrategy = null;
                        textEditor.SyntaxHighlighting = HighlightingLoader.Load(HighlightingLoader.LoadXshd(new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "HighlightRefs\\VB.xshd")), HighlightingManager.Instance);
                        break;
                    case 3:
                        foldingUpdateTimer.Stop();
                        foldingStrategy = null;
                        textEditor.SyntaxHighlighting = HighlightingLoader.Load(HighlightingLoader.LoadXshd(new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "HighlightRefs\\VB.xshd")), HighlightingManager.Instance);
                        break;
                    case 4:
                        foldingUpdateTimer.Stop();
                        foldingStrategy = new SSFoldingStrategy();
                        foldingUpdateTimer.Tick += delegate
                        {
                            foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
                        };
                        foldingUpdateTimer.Start();
                        foldingUpdateTimer.Start();
                        Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory + "SS.xshd");
                        textEditor.SyntaxHighlighting = HighlightingLoader.Load(HighlightingLoader.LoadXshd(new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "HighlightRefs\\SS.xshd")), HighlightingManager.Instance);
                        break;
                }

                textEditor.Text = File.ReadAllText(CurrentProject.Program);
            }

            if (Configuration.Theme == (int)ThemeType.Dark)
                textEditor.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        private void TextEditor_TextChanged(object sender, EventArgs e)
        {
            if (Configuration.EnableAutoSave && CurrentProject != null && CurrentProject != ProjectIniReader.UnknownProject)
            {
                File.WriteAllText(CurrentProject.Program, textEditor.Text);
            }
        }

        private void OpenFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SiCode IDE Project files|*.ini";
            if (ofd.ShowDialog() == true)
            {
                CurrentProject = ProjectIniReader.OpenProject(ofd.FileName);
                if (!CurrentProject.Equals(ProjectIniReader.UnknownProject))
                {
                    titleTextBlock.Text = "SiCode IDE - " + CurrentProject.Name;

                    switch ((int)CurrentProject.ProjectType)
                    {
                        case 0:
                            textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(textEditor.Options);
                            foldingUpdateTimer.Stop();
                            foldingStrategy = new BraceFoldingStrategy();
                            foldingUpdateTimer.Tick += delegate
                            {
                                foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
                            };
                            foldingUpdateTimer.Start();
                            textEditor.SyntaxHighlighting = HighlightingLoader.Load(HighlightingLoader.LoadXshd(new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "HighlightRefs\\CS.xshd")), HighlightingManager.Instance);
                            break;
                        case 1:
                            textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(textEditor.Options);
                            foldingUpdateTimer.Stop();
                            foldingStrategy = new BraceFoldingStrategy();
                            foldingUpdateTimer.Tick += delegate
                            {
                                foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
                            };
                            foldingUpdateTimer.Start();
                            textEditor.SyntaxHighlighting = HighlightingLoader.Load(HighlightingLoader.LoadXshd(new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "HighlightRefs\\CS.xshd")), HighlightingManager.Instance);
                            break;
                        case 2:
                            foldingUpdateTimer.Stop();
                            foldingStrategy = null;
                            textEditor.SyntaxHighlighting = HighlightingLoader.Load(HighlightingLoader.LoadXshd(new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "HighlightRefs\\VB.xshd")), HighlightingManager.Instance);
                            break;
                        case 3:
                            foldingUpdateTimer.Stop();
                            foldingStrategy = null;
                            textEditor.SyntaxHighlighting = HighlightingLoader.Load(HighlightingLoader.LoadXshd(new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "HighlightRefs\\VB.xshd")), HighlightingManager.Instance);
                            break;
                        case 4:
                            foldingUpdateTimer.Stop();
                            foldingStrategy = new SSFoldingStrategy();
                            foldingUpdateTimer.Tick += delegate
                            {
                                foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
                            };
                            foldingUpdateTimer.Start();
                            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory + "SS.xshd");
                            textEditor.SyntaxHighlighting = HighlightingLoader.Load(HighlightingLoader.LoadXshd(new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "HighlightRefs\\SS.xshd")), HighlightingManager.Instance);
                            break;
                    }

                    textEditor.Text = File.ReadAllText(CurrentProject.Program);
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewProjectDialog w = new NewProjectDialog();
            w.Show();
            w.Closing += (s, ev) => 
            {
                if (!w.CancelProjectCreation)
                {
                    ProjectIniReader.CreateProject(w.pjNameTb.Text, (ProjectType)w.pjTypeCb.SelectedIndex, w.pjLocTb.Text);
                    CurrentProject = ProjectIniReader.OpenProject(w.pjLocTb.Text + @"\" + w.pjNameTb.Text + ".ini");

                    if (!CurrentProject.Equals(ProjectIniReader.UnknownProject))
                    {
                        titleTextBlock.Text = "SiCode IDE - " + CurrentProject.Name;

                        switch ((int)CurrentProject.ProjectType)
                        {
                            case 0:
                                textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(textEditor.Options);
                                foldingUpdateTimer.Stop();
                                foldingStrategy = new BraceFoldingStrategy();
                                foldingUpdateTimer.Tick += delegate
                                {
                                    foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
                                };
                                foldingUpdateTimer.Start();
                                textEditor.SyntaxHighlighting = HighlightingLoader.Load(HighlightingLoader.LoadXshd(new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "HighlightRefs\\CS.xshd")), HighlightingManager.Instance);
                                break;
                            case 1:
                                textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(textEditor.Options);
                                foldingUpdateTimer.Stop();
                                foldingStrategy = new BraceFoldingStrategy();
                                foldingUpdateTimer.Tick += delegate
                                {
                                    foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
                                };
                                foldingUpdateTimer.Start();
                                textEditor.SyntaxHighlighting = HighlightingLoader.Load(HighlightingLoader.LoadXshd(new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "HighlightRefs\\CS.xshd")), HighlightingManager.Instance);
                                break;
                            case 2:
                                foldingUpdateTimer.Stop();
                                foldingStrategy = null;
                                textEditor.SyntaxHighlighting = HighlightingLoader.Load(HighlightingLoader.LoadXshd(new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "HighlightRefs\\VB.xshd")), HighlightingManager.Instance);
                                break;
                            case 3:
                                foldingUpdateTimer.Stop();
                                foldingStrategy = null;
                                textEditor.SyntaxHighlighting = HighlightingLoader.Load(HighlightingLoader.LoadXshd(new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "HighlightRefs\\VB.xshd")), HighlightingManager.Instance);
                                break;
                            case 4:
                                foldingUpdateTimer.Stop();
                                foldingStrategy = new SSFoldingStrategy();
                                foldingUpdateTimer.Tick += delegate
                                {
                                    foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
                                };
                                foldingUpdateTimer.Start();
                                textEditor.SyntaxHighlighting = HighlightingLoader.Load(HighlightingLoader.LoadXshd(new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + "HighlightRefs\\SS.xshd")), HighlightingManager.Instance);
                                
                                cMenu.IsEnabled = false;
                                
                                break;
                        }

                        textEditor.Text = File.ReadAllText(CurrentProject.Program);
                    }
                }
            };
        }
        

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (CurrentProject == null || CurrentProject == ProjectIniReader.UnknownProject)
            {
                NotificationHelper.ShowNotification(typeof(MainWindow), $"Please open a project before using the '{((MenuItem)sender).Header}' option", System.Windows.MessageBoxImage.Error);
            }
            else
            {
                File.WriteAllText(CurrentProject.Program, textEditor.Text);
                switch ((int)CurrentProject.ProjectType)
                {
                    case 0:
                        var code3 = csc.Compile(CurrentProject);
                        SetStatus(code3);
                        break;
                    case 1:
                        var code2 = csc.Compile(CurrentProject);
                        SetStatus(code2);
                        break;
                    case 2:
                        var code1 = vbc.Compile(CurrentProject);
                        SetStatus(code1);
                        break;
                    case 3:
                        var code = vbc.Compile(CurrentProject);
                        SetStatus(code);
                        break;
                }
            }
        }

        public void SetStatus(int code)
        {
            switch (code)
            {
                case -1:
                    compileStatus.Text = "Unknown error";
                    break;
                case 0:
                    compileStatus.Text = "Compile happened successfully.";
                    break;
                case 1:
                    compileStatus.Text = "Compile happened with errors.";
                    break;

            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            if (CurrentProject == null || CurrentProject == ProjectIniReader.UnknownProject)
            {
                NotificationHelper.ShowNotification(typeof(MainWindow), $"Please open a project before using the '{((MenuItem)sender).Header}' option", System.Windows.MessageBoxImage.Error);
            }
            else
            {
                if (CurrentProject.ProjectType != ProjectType.SSApplication)
                {
                    Process.Start($@"{CurrentProject.ProjectDir}\Binary\{CurrentProject.Name}.exe");
                }
                else
                {
                    var ocii = OptionalComponentManager.GetComponentInstallPath(OptionalComponent.SSInterpreter, Architecture.AnyCPU);
                    if (ocii != null)
                    {
                        Process.Start(ocii.ComponentInstallPath + "\\SSharpInterpreter.exe", CurrentProject.Program);
                    }
                    else
                    {
                        new ToastContentBuilder()
                                    .AddText("You need to download the S# interpreter optional component to run S# applications.")
                                    .AddText("SiCode IDE didn't found S# interpreter installed. To do this, go to Options -> Download optional components & select 'SSInterpreter' in the Component combo box. After that, click Install.")
                                    .Show();
                    }
                }
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            if (CurrentProject == null || CurrentProject == ProjectIniReader.UnknownProject)
            {
                NotificationHelper.ShowNotification(typeof(MainWindow), $"Please open a project before using the '{((MenuItem)sender).Header}' option", System.Windows.MessageBoxImage.Error);
            }
            else
            {
                File.WriteAllText(CurrentProject.Program, textEditor.Text);
            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            if (CurrentProject == null || CurrentProject == ProjectIniReader.UnknownProject)
            {
                NotificationHelper.ShowNotification(typeof(MainWindow), $"Please open a project before using the '{((MenuItem)sender).Header}' option", System.Windows.MessageBoxImage.Error);
            }
            else
            {
                switch ((int)CurrentProject.ProjectType)
                {
                    case 0:
                        LastCompileErrors w = new LastCompileErrors(csc.LastCompileErrors);
                        w.Show();
                        break;
                    case 1:
                        LastCompileErrors w1 = new LastCompileErrors(csc.LastCompileErrors);
                        w1.Show();
                        break;
                    case 2:
                        LastCompileErrors w2 = new LastCompileErrors(vbc.LastCompileErrors);
                        w2.Show();
                        break;
                    case 3:
                        LastCompileErrors w3 = new LastCompileErrors(vbc.LastCompileErrors);
                        w3.Show();
                        break;
                }
            }
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            if (CurrentProject == null || CurrentProject == ProjectIniReader.UnknownProject)
            {
                NotificationHelper.ShowNotification(typeof(MainWindow), $"Please open a project before using the '{((MenuItem)sender).Header}' option", System.Windows.MessageBoxImage.Error);
            }
            else
            {
                switch ((int)CurrentProject.ProjectType)
                {
                    case 0:
                        LastCompileLogs w = new LastCompileLogs(csc.LastCompileLogs);
                        w.Show();
                        break;
                    case 1:
                        LastCompileLogs w1 = new LastCompileLogs(csc.LastCompileLogs);
                        w1.Show();
                        break;
                    case 2:
                        LastCompileLogs w2 = new LastCompileLogs(vbc.LastCompileLogs);
                        w2.Show();
                        break;
                    case 3:
                        LastCompileLogs w3 = new LastCompileLogs(vbc.LastCompileLogs);
                        w3.Show();
                        break;
                }
            }
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {

            if (CurrentProject == null || CurrentProject == ProjectIniReader.UnknownProject)
            {
                NotificationHelper.ShowNotification(typeof(MainWindow), $"Please open a project before using the '{((MenuItem)sender).Header}' option", System.Windows.MessageBoxImage.Error);
            }
            else
            {
                Dialogs.ReferencedAssemblies w3 = new Dialogs.ReferencedAssemblies(CurrentProject);
                w3.Show();
            }
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            OptionsWindow w = new OptionsWindow();
            w.Show();
        }

        private void TitleBar_CloseClicked(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);

        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            HomeWindow w = new HomeWindow();
            w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            w.Show();
            this.Close();
        }

        private void cMenu6_Click(object sender, RoutedEventArgs e)
        {
            DownloadComponents w = new DownloadComponents();
            w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            w.Show();
        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            AskChatGPT w = new AskChatGPT();
            w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            w.Show();
        }
    }
}
