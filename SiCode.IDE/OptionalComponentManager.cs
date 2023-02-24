using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SiCode.IDE
{
    // Enums & structs
    public enum Architecture
    {
        x64,
        x86,
        ARM64,
        AnyCPU,
    }

    public enum OptionalComponent
    {
        Python3,
        SSInterpreter
    }

    public class OCInstallInfo
    {
        public Architecture InstalledArchitecture;
        public OptionalComponent InstalledComponent;
        public string ComponentInstallPath;
    }

    // Main class
    public class OptionalComponentManager
    {
        /// <summary>
        /// The folder where optional components is installed.
        /// </summary>
        public static string OptionalComponentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SiCodeIDE\\OptionalComponents";

        /// <summary>
        /// Checks if the optional components folder exists. If not, create it.
        /// </summary>
        public static void CheckForOCFolder()
        {
            if (!Directory.Exists(OptionalComponentsFolderPath))
                Directory.CreateDirectory(OptionalComponentsFolderPath);
        }

        /// <summary>
        /// Download & extract an optional component with the provided architecture.
        /// </summary>
        /// <param name="oc">The optional component to download & extract</param>
        /// <param name="arch">The architecture of the optional component</param>
        public static void Download(OptionalComponent oc, Architecture arch)
        {
            CheckForOCFolder();

            string extractPath = OptionalComponentsFolderPath + "\\" + oc + "\\" + arch;
            string archiveDownloadPath = Path.GetTempFileName() + ".zip";
            string downloadLink = GetDownloadLink(oc, arch);

            WebClient wc = new WebClient();

            if (arch == Architecture.AnyCPU)
            {
                extractPath = OptionalComponentsFolderPath + "\\" + oc;
            }

            // Download the file, save it to a temp file, extract the content & delete temp archive.
            Thread dT = new Thread(new ThreadStart(() =>
            {
                try
                {
                    if (downloadLink != null || downloadLink != "")
                    {
                        NotificationHelper.ShowNotification(typeof(OptionalComponentManager), "The install of " + oc + " with the " + arch + " architecture has been started.", MessageBoxImage.Information);

                        wc.DownloadFile(downloadLink, archiveDownloadPath);
                        ZipFile.ExtractToDirectory(archiveDownloadPath, extractPath);
                        File.Delete(archiveDownloadPath);

                        NotificationHelper.ShowNotification(typeof(OptionalComponentManager), "The install of " + oc + " with the " + arch + " architecture has been completed!", MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    NotificationHelper.ShowNotification(typeof(OptionalComponentManager), $"Unable to download optional component!\n\n{ex}", MessageBoxImage.Error);
                }
            }));
            dT.Start();
        }

        /// <summary>
        /// Check if an optional component is installed.
        /// </summary>
        /// <param name="oc">The optional component to check</param>
        /// <param name="preferedArchitecture">The prefered architecture (the architecture than will be searched first) (x64 is set as default)</param>
        /// <returns>The optional component install information. If the o. c. is not installed, the null value will be returned.</returns>
        public static OCInstallInfo GetComponentInstallPath(OptionalComponent oc, Architecture preferedArchitecture = Architecture.x64)
        {
            // Try to find component with prefered architecture
            string pComponentPath = OptionalComponentsFolderPath + "\\" + oc;
            if (preferedArchitecture != Architecture.AnyCPU)
            {
                pComponentPath += "\\" + preferedArchitecture;
            }

            if (Directory.Exists(pComponentPath))
            {
                return new OCInstallInfo { ComponentInstallPath = pComponentPath, InstalledArchitecture = preferedArchitecture, InstalledComponent = oc };
            }

            // Try to find component with x64 architecture
            string x64ComponentPath = OptionalComponentsFolderPath + "\\" + Architecture.x64;

            if (Directory.Exists(x64ComponentPath))
            {
                return new OCInstallInfo { ComponentInstallPath = x64ComponentPath, InstalledArchitecture = Architecture.x64, InstalledComponent = oc };
            }
            // Try to find component with x86 architecture
            string x86ComponentPath = OptionalComponentsFolderPath + "\\" + Architecture.x86;

            if (Directory.Exists(x86ComponentPath))
            {
                return new OCInstallInfo { ComponentInstallPath = x86ComponentPath, InstalledArchitecture = Architecture.x86, InstalledComponent = oc };
            }
            // Try to find component with ARM64 architecture
            string arm64ComponentPath = OptionalComponentsFolderPath + "\\" + Architecture.ARM64;

            if (Directory.Exists(arm64ComponentPath))
            {
                return new OCInstallInfo { ComponentInstallPath = arm64ComponentPath, InstalledArchitecture = Architecture.ARM64, InstalledComponent = oc };
            }
            // Try to find component with AnyCPU architecture
            string acComponentPath = OptionalComponentsFolderPath + "\\" + oc;

            if (Directory.Exists(acComponentPath))
            {
                return new OCInstallInfo { ComponentInstallPath = acComponentPath, InstalledArchitecture = Architecture.AnyCPU, InstalledComponent = oc };
            }

            // Not found, so return null.
            return null;
        }

        // Private
        static string GetDownloadLink(OptionalComponent oc, Architecture arch)
        {
            switch (oc)
            {
                case OptionalComponent.Python3:
                    return GetPythonDownloadLink(arch);
                case OptionalComponent.SSInterpreter:
                    return GetSSInterpreterDownloadLink(arch);
                default:
                    return "";
            }
        }

        static string GetPythonDownloadLink(Architecture arch)
        {
            switch (arch)
            {
                case Architecture.x64:
                    return "https://www.python.org/ftp/python/3.11.2/python-3.11.2-embed-amd64.zip";
                case Architecture.x86:
                    return "https://www.python.org/ftp/python/3.11.2/python-3.11.2-embed-win32.zip";
                case Architecture.ARM64:
                    return "https://www.python.org/ftp/python/3.11.2/python-3.11.2-embed-arm64.zip";
                case Architecture.AnyCPU:
                    new ToastContentBuilder()
                                .AddHeader("ocm", "Optional Component Manager", "")
                                .AddText("The component Python3 can't be downloaded for the " + arch + " architecture.")
                                .Show();
                    return "";
                default:
                    // Most of machines in the world uses x64 as architecture. So returns the x64 download link.
                    return "https://www.python.org/ftp/python/3.11.2/python-3.11.2-embed-amd64.zip";
            }
        }

        static string GetSSInterpreterDownloadLink(Architecture arch)
        {
            // The S# interpreter is compiled for any CPU. so whatever the architecture, return this link.
            return "https://raw.githubusercontent.com/SipaaKernel-Project/SiCode-IDE/main/Binary/SSharpInterpreterBin.zip";
        }
    }
}
