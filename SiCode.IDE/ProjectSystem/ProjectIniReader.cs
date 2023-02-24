using SiCode.IDE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiCodeIDE.ProjectSystem
{
    public enum ProjectType
    {
        CSharpApplication,
        CSharpLibrary,
        VisualBasicApplication,
        VisualBasicLibrary,
        SSApplication,
    }
    public class Project
    {
        public string Name;
        public ProjectType ProjectType;
        public string ProjectPath;
        public string ProjectDir;
        public string Program;
        public ReferencedAssemblies ReferencedAssemblies;
    }

    /// <summary>
    /// Parser for SiCode IDE projects
    /// </summary>
    public class ProjectIniReader
    {
        /// <summary>
        /// This project is returned when the project parser fails to read the project.
        /// </summary>
        public static Project UnknownProject = new Project() { Name = "Unknown", ProjectDir = "Unknown", ProjectPath = "Unknown", ProjectType = ProjectType.CSharpApplication, Program = "Unknown" };

        /// <summary>
        /// Create & save a project
        /// </summary>
        /// <param name="Name">The project name</param>
        /// <param name="ProjectType">The type of the project</param>
        /// <param name="ProjectDir">The project directory</param>
        public static void CreateProject(string Name, ProjectType ProjectType, string ProjectDir)
        {
            if (!Directory.Exists(ProjectDir))
            {
                Directory.CreateDirectory(ProjectDir);
            }

            StreamWriter w = new StreamWriter(ProjectDir + @"\" + Name + ".ini");
            w.WriteLine("[SiCodeProject]");
            w.WriteLine($"ProjectName={Name}");
            w.WriteLine($"ProjectType={(int)ProjectType}");
            w.Close();

            // Program file
            if (ProjectType == ProjectType.SSApplication)
            {
                StreamWriter w2 = new StreamWriter(ProjectDir + @"\Program.ss");
                w2.WriteLine("print(2 + 2)");
                w2.Close();
            }
            else if (ProjectType == ProjectType.CSharpApplication || ProjectType == ProjectType.CSharpLibrary)
            {
                StreamWriter w2 = new StreamWriter(ProjectDir + @"\Program.cs");
                switch (ProjectType)
                {
                    case ProjectType.CSharpApplication:
                        w2.WriteLine(GlobalConstants.DefaultCSharpConsoleAppProgramCode);
                        break;
                    case ProjectType.CSharpLibrary:
                        w2.WriteLine(GlobalConstants.DefaultCSharpLibraryProgramCode);
                        break;
                }
                w2.Close();
            }
            else if (ProjectType == ProjectType.VisualBasicApplication || ProjectType == ProjectType.VisualBasicLibrary)
            {
                StreamWriter w2 = new StreamWriter(ProjectDir + @"\Program.vb");
                switch (ProjectType)
                {
                    case ProjectType.VisualBasicApplication:
                        w2.WriteLine(GlobalConstants.DefaultVisualBasicApplicationProgramCode);
                        break;
                    case ProjectType.VisualBasicLibrary:
                        w2.WriteLine(GlobalConstants.DefaultVisualBasicLibraryProgramCode);
                        break;
                }
                w2.Close();
            }
        }

        /// <summary>
        /// Read a project and put the infos into a Project class.
        /// </summary>
        /// <param name="file">The project to read</param>
        /// <returns></returns>
        public static Project OpenProject(string file)
        {
            Project p = new Project();
            FileInfo f = new FileInfo(file);
            
            if (f.Exists)
            {
                p.ProjectPath = f.FullName;
                p.ProjectDir = f.Directory.FullName;
                if (f.Extension.EndsWith("ini"))
                {
                    if (String.IsNullOrEmpty(File.ReadAllText(file)))
                    {
                        NotificationHelper.ShowNotification(typeof(ProjectIniReader), "The SiCode IDE project reader can't read empty/non existant files.", System.Windows.MessageBoxImage.Error);
                        return UnknownProject;
                    }
                    else
                    {
                        string[] lines = File.ReadAllLines(f.FullName);
                        if (lines[0] == "[SiCodeProject]")
                        {
                            for (int i = 1; i < lines.Length; i++)
                            {
                                string[] lineSplitted = lines[i].Split('=');
                                string name = lineSplitted[0];
                                string value = lineSplitted[1];
                                switch (name)
                                {
                                    case "ProjectName":
                                        p.Name = value;
                                        break;
                                    case "ProjectType":
                                        p.ProjectType = (ProjectType)int.Parse(value);
                                        break;
                                }
                            }
                            if (p.ProjectType == ProjectType.CSharpApplication || p.ProjectType == ProjectType.CSharpLibrary)
                            {
                                if (File.Exists(p.ProjectDir + @"\Program.cs"))
                                {
                                    p.Program = p.ProjectDir + @"\Program.cs";
                                    p.ReferencedAssemblies = new ReferencedAssemblies(p, true);
                                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $@"\SiCodeIDE\RecentProjects\{p.Name}.scrp", p.ProjectPath);
                                    return p;
                                }
                                else
                                {
                                    NotificationHelper.ShowNotification(typeof(ProjectIniReader), "The project than you want open is valid, but no program file found in the project directory.", System.Windows.MessageBoxImage.Error);
                                    return UnknownProject;
                                }
                            }
                            if (p.ProjectType == ProjectType.VisualBasicApplication || p.ProjectType == ProjectType.VisualBasicLibrary)
                            {
                                if (File.Exists(p.ProjectDir + @"\Program.vb"))
                                {
                                    p.Program = p.ProjectDir + @"\Program.vb";
                                    p.ReferencedAssemblies = new ReferencedAssemblies(p, true);
                                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $@"\SiCodeIDE\RecentProjects\{p.Name}.scrp", p.ProjectPath);
                                    return p;
                                }
                                else
                                {
                                    NotificationHelper.ShowNotification(typeof(ProjectIniReader), "The project than you want open is valid, but no program file found in the project directory.", System.Windows.MessageBoxImage.Error);
                                    return UnknownProject;
                                }
                            }
                            else if (p.ProjectType == ProjectType.SSApplication)
                            {
                                if (File.Exists(p.ProjectDir + @"\Program.ss"))
                                {
                                    p.Program = p.ProjectDir + @"\Program.ss";
                                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $@"\SiCodeIDE\RecentProjects\"))
                                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $@"\SiCodeIDE\RecentProjects\");
                                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $@"\SiCodeIDE\RecentProjects\{p.Name}.scrp", p.ProjectPath);
                                    return p;
                                }
                                else
                                {
                                    NotificationHelper.ShowNotification(typeof(ProjectIniReader), "The project than you want open is valid, but no program file found in the project directory.", System.Windows.MessageBoxImage.Error);
                                    return UnknownProject;
                                }
                            }
                        }
                    }
                    NotificationHelper.ShowNotification(typeof(ProjectIniReader), "The project than you want open is invalid.", System.Windows.MessageBoxImage.Error);
                    return UnknownProject;
                }

                NotificationHelper.ShowNotification(typeof(ProjectIniReader), "The project than you want open is invalid.", System.Windows.MessageBoxImage.Error);
                return UnknownProject;
            }

            NotificationHelper.ShowNotification(typeof(ProjectIniReader), "The project than you want open doens't exists!", System.Windows.MessageBoxImage.Error);
            return UnknownProject;
        }
    }
}
