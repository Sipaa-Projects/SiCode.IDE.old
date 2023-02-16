using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using dr = System.Drawing;
using System.Windows.Threading;
using Microsoft.Win32;
using System.IO;
using SiCodeIDE.ProjectSystem;
using SiCode.IDE.Dialogs;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using SiCodeIDE;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml;
using Wpf.Ui.Appearance;

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
                        break;
                    case 1:
                        textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(textEditor.Options);
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
            if (Configuration.EnableAutoSave)
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
                            break;
                        case 1:
                            textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(textEditor.Options);
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
            }
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow w = new AboutWindow();
            w.Show();
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
                                break;
                            case 1:
                                textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(textEditor.Options);
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
                MessageBox.Show("Please open a project before doing this action.", "SiCode IDE", MessageBoxButton.OK);
            }
            else
            {
                File.WriteAllText(CurrentProject.Program, textEditor.Text);
                switch ((int)CurrentProject.ProjectType)
                {
                    case 0:
                        csc.Compile(CurrentProject);
                        break;
                    case 1:
                        csc.Compile(CurrentProject);
                        break;
                    case 2:
                        vbc.Compile(CurrentProject);
                        break;
                    case 3:
                        vbc.Compile(CurrentProject);
                        break;
                }
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            if (CurrentProject == null || CurrentProject == ProjectIniReader.UnknownProject)
            {
                MessageBox.Show("Please open a project before doing this action.", "SiCode IDE", MessageBoxButton.OK);
            }
            else
            {
                Process.Start($@"{CurrentProject.ProjectDir}\Binary\{CurrentProject.Name}.exe");
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(CurrentProject.Program, textEditor.Text);
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            if (CurrentProject == null || CurrentProject == ProjectIniReader.UnknownProject)
            {
                MessageBox.Show("Please open a project before doing this action.", "SiCode IDE", MessageBoxButton.OK);
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
                MessageBox.Show("Please open a project before doing this action.", "SiCode IDE", MessageBoxButton.OK);
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
                MessageBox.Show("Please open a project before doing this action.", "SiCode IDE", MessageBoxButton.OK);
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
    }
}
