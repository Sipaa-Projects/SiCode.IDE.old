using Microsoft.CSharp;
using SiCodeIDE.ProjectSystem;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace SiCodeIDE
{
    internal class VbCompiler
    {
        public string LastCompileLogs = "";
        public string LastCompileErrors = "";
        public void Compile(Project p)
        {
            VBCodeProvider codeProvider = new VBCodeProvider();
            ICodeCompiler icc = codeProvider.CreateCompiler();
            string Output = $@"{p.ProjectDir}\Binary\{p.Name}.exe";
            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            if (p.ProjectType == ProjectType.VisualBasicApplication || p.ProjectType == ProjectType.VisualBasicLibrary) { parameters.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(Form)).Location); foreach (AssemblyName a in Assembly.GetAssembly(typeof(Form)).GetReferencedAssemblies()) { Assembly a2 = Assembly.Load(a); parameters.ReferencedAssemblies.Add(a2.Location); } }
            if (p.ProjectType == ProjectType.VisualBasicLibrary) 
            {
                Output = $@"{p.ProjectDir}\Binary\{p.Name}.dll";
                parameters.GenerateExecutable = false;
            }
            else if (p.ProjectType == ProjectType.VisualBasicApplication)
            {
                parameters.GenerateExecutable = true;
            }
            else
            {
                MessageBox.Show("The Visual Basic compiler can't compile Visual Basic applications/libraries.",
                    "SiCode IDE Visual Basic Compiler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (p.ReferencedAssemblies.GetReferencedAssembliesCount() >= 1)
            {
                var ra = p.ReferencedAssemblies.GetReferencedAssemblies();
                foreach (string file in ra)
                {
                    parameters.ReferencedAssemblies.Add(file);
                }
            }
            parameters.OutputAssembly = Output;
            CompilerResults results = icc.CompileAssemblyFromSource(parameters, File.ReadAllText(p.Program));
            LastCompileLogs = "";
            foreach (string s in results.Output)
            {
                LastCompileLogs += s;
            }

            if (results.Errors.Count > 0)
            {
                foreach (CompilerError CompErr in results.Errors)
                {
                    LastCompileErrors = "Compiler errors :";
                    LastCompileErrors +=
                                "\nLine number " + CompErr.Line +
                                ", Error Number: " + CompErr.ErrorNumber +
                                ", '" + CompErr.ErrorText + ";" +
                                Environment.NewLine + Environment.NewLine;
                }
                MessageBox.Show("Some errors happened during compiling. To see the errors, go to Compiler -> See last compile errors.", "SiCode IDE Visual Basic Compiler");
            }
        }
    }
}
