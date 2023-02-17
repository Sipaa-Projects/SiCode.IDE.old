using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiCodeIDE
{
    internal class GlobalConstants
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
