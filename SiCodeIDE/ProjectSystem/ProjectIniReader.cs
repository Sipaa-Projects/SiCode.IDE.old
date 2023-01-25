using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiCodeIDE.ProjectSystem
{
    internal enum ProjectType
    {
        CSharpApplication,
        CSharpLibrary,
        VisualBasicApplication,
        VisualBasicLibrary,
        Website,
    }
    internal struct Project
    {
        public string Name;
        public ProjectType ProjectType;
        public string ProjectPath;
        public string ProjectDir;
        public string Program;
        public ReferencedAssemblies ReferencedAssemblies;
    }
    internal class ProjectIniReader
    {
        public static Project UnknownProject = new Project() { Name = "Unknown", ProjectDir = "Unknown", ProjectPath = "Unknown", ProjectType = ProjectType.CSharpApplication, Program = "Unknown" };

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
            if (ProjectType == ProjectType.Website)
            {
                StreamWriter w2 = new StreamWriter(ProjectDir + @"\index.html");
                w2.WriteLine(GlobalConstants.DefaultWebsiteCode);
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
                        MessageBox.Show("You can't create a project by just making a empty .ini file", "SiCodeIDE.ProjectSystem.ProjectIniReader");
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
                                    return p;
                                }
                                else
                                {
                                    MessageBox.Show("The project than you want open is valid, but no program file found in the project directory.", "SiCodeIDE.ProjectSystem.ProjectIniReader");
                                    return UnknownProject;
                                }
                            }
                            if (p.ProjectType == ProjectType.VisualBasicApplication || p.ProjectType == ProjectType.VisualBasicLibrary)
                            {
                                if (File.Exists(p.ProjectDir + @"\Program.vb"))
                                {
                                    p.Program = p.ProjectDir + @"\Program.vb";
                                    return p;
                                }
                                else
                                {
                                    MessageBox.Show("The project than you want open is valid, but no program file found in the project directory.", "SiCodeIDE.ProjectSystem.ProjectIniReader");
                                    return UnknownProject;
                                }
                            }
                            else if (p.ProjectType == ProjectType.Website)
                            {
                                if (File.Exists(p.ProjectDir + @"\index.html"))
                                {
                                    p.Program = p.ProjectDir + @"\index.html";
                                    return p;
                                }
                                else
                                {
                                    MessageBox.Show("The project than you want open is valid, but no index file found in the project directory.", "SiCodeIDE.ProjectSystem.ProjectIniReader");
                                    return UnknownProject;
                                }
                            }
                        }
                    }
                    MessageBox.Show("The project than you want open is invalid.", "SiCodeIDE.ProjectSystem.ProjectIniReader");
                    return UnknownProject;
                }

                MessageBox.Show("The project than you want open is invalid.", "SiCodeIDE.ProjectSystem.ProjectIniReader");
                return UnknownProject;
            }

            MessageBox.Show("The project than you want open doens't exists!", "SiCodeIDE.ProjectSystem.ProjectIniReader");
            return UnknownProject;
        }
    }
}
