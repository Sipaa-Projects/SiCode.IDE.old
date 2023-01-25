using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiCodeIDE.ProjectSystem
{
    internal class ReferencedAssemblies
    {
        private List<string> refassemblies;
        private Project p;

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
                foreach(string file in lines)
                {
                    if (File.Exists(file))
                    {
                        refassemblies.Add(file);
                    }
                    else
                    {
                        MessageBox.Show("The referenced assembly " + file + "doens't exists.", "SiCode IDE");
                    }
                }
            }
            else
            {
                Console.WriteLine($"There are no custom referenced assemblies for {p.Name}. A custom referenced assemblies file will be created.");
                File.Create(refAssembliesFile.FullName);
                MessageBox.Show("The IDE needs to be restarted to complete the creation of a referenced assemblies file for " + p.Name, "SiCode IDE");
            }
        }
    }
}
