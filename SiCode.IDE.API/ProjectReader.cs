using Microsoft.Toolkit.Uwp.Notifications;

namespace SiCode.IDE.API
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
        /// This project is returned when the project parser fails to read the provided project.
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
                        SendToastNotification("Error", "The SiCode IDE project reader can't read empty/non existant files.");
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
                                    SendToastNotification("Error", "The project than you want open is valid, but no program file found in the project directory.");
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
                                    SendToastNotification("Error", "The project than you want open is valid, but no program file found in the project directory.");
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
                                    SendToastNotification("Error", "The project than you want open is valid, but no program file found in the project directory.");
                                    return UnknownProject;
                                }
                            }
                        }
                    }
                    SendToastNotification("Error", "The project than you want open is invalid.");
                    return UnknownProject;
                }

                SendToastNotification("Error", "The project than you want open is invalid.");
                return UnknownProject;
            }

            SendToastNotification("Error", "The project than you want open doens't exists!");
            return UnknownProject;
        }

        static void SendToastNotification(string title, string text)
        {
            new ToastContentBuilder()
                .AddText(title)
                .AddText(text)
                .Show();
        }
    }
    public class ReferencedAssemblies
    {
        private List<string> refassemblies;
        private Project p;

        static void SendToastNotification(string title, string text)
        {
            new ToastContentBuilder()
                .AddText(title)
                .AddText(text)
                .Show();
        }

        public ReferencedAssemblies(Project p, bool directlyGetAssemblies = true)
        {
            this.p = p;
            this.refassemblies = new List<string>();
            if (directlyGetAssemblies)
            {
                ReadReferencedAssembliesFile();
            }
        }

        public void SaveReferencedAssemblies()
        {
            StreamWriter w = new StreamWriter(p.ProjectDir + @"\ReferencedAssemblies.scra");
            foreach (string file in refassemblies)
            {
                w.WriteLine(file);
            }
            w.Close();
            Console.WriteLine("Referenced assemblies for " + p.Name + " has been saved!");
        }

        public void AddReferencedAssembly(string file)
        {
            refassemblies.Add(file);
        }

        public void RemoveReferencedAssembly(string file)
        {
            refassemblies.Remove(file);
        }

        public int GetReferencedAssembliesCount() { return refassemblies.Count; }

        public List<string> GetReferencedAssemblies()
        {
            return refassemblies;
        }

        public void ReadReferencedAssembliesFile()
        {
            FileInfo refAssembliesFile = new FileInfo(p.ProjectDir + @"\ReferencedAssemblies.scra");

            if (refAssembliesFile.Exists)
            {
                string[] lines = File.ReadAllLines(refAssembliesFile.FullName);
                foreach (string file in lines)
                {
                    if (File.Exists(file))
                    {
                        refassemblies.Add(file);
                    }
                    else
                    {
                        SendToastNotification("Error", "The project than you want open doens't exists!");
                    }
                }
            }
            else
            {
                Console.WriteLine($"There are no custom referenced assemblies for {p.Name}. A custom referenced assemblies file will be created.");
                File.Create(refAssembliesFile.FullName);
                SendToastNotification("Info", "You need to restart SiCode IDE to edit reference assemblies");
            }
        }
    }
    public class GlobalConstants
    {
        private static char hi = '"';

        public static string DefaultVisualBasicApplicationProgramCode =
            $"Imports System\n" +
            $"" +
            $"Module Program\n" +
            $"   Sub Main(args As String())\n" +
            $"       Console.WriteLine(2 + 2);\n" +
            $"       Console.ReadKey();\n" +
            $"   End Sub\n" +
            $"End Module";

        public static string DefaultVisualBasicLibraryProgramCode =
            $"Imports System\n" +
            $"" +
            $"Public Class Math\n" +
            $"   Public Shared Function Sum(a As Integer, b as Integer) As Integer\n" +
            $"       Return a + b\n" +
            $"   End Function\n" +
            $"End Class";

        public const string DefaultCSharpConsoleAppProgramCode =
            "using System;\n" +
            "namespace Application\n" +
            "{\n" +
            "   public class Program\n" +
            "   {\n" +
            "       static void Main(string[] args)\n" +
            "       {\n" +
            "           Console.WriteLine(2 + 2);\n" +
            "           Console.ReadKey();\n" +
            "       }\n" +
            "   }\n" +
            "}";
        public const string DefaultCSharpLibraryProgramCode =
            "using System;\n" +
            "namespace Library\n" +
            "{\n" +
            "   public class Math\n" +
            "   {\n" +
            "       public static int Sum(int a, int b)\n" +
            "       {\n" +
            "           return a + b;\n" +
            "       }\n" +
            "   }\n" +
            "}";
        public const string DefaultWebsiteCode =
            "<!DOCTYPE html>\n" +
            "<head>\n" +
            "<\\head>\n" +
            "<body>" +
            "   <h1>Here is a new website made with SiCode IDE<\\h1>\n" +
            "<\\body>\n";
    }
}