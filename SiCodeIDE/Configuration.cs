using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiCodeIDE
{
    internal class Configuration
    {
        public static bool EnableVisualEffects { get; set; } = true;
        public static bool EnableAutoSave { get; set; } = true;

        public static string ConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SiCodeIDE\\Config.ini";
        public static string ConfigDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SiCodeIDE";
        const string DefaultConfig =
            "[SiCodeConfiguration]\n" +
            "EnableVisualEffects=false\n" +
            "EnableAutoSave=true";

        public static void SaveConfig()
        {
            StreamWriter w = new StreamWriter(ConfigFile);
            w.WriteLine("[SiCodeConfiguration]");
            w.WriteLine("EnableVisualEffects=" + EnableVisualEffects);
            w.WriteLine("EnableAutoSave=" + EnableVisualEffects);
            w.Close();
        }

        public static void CreateDefaultConfig()
        {
            if (!Directory.Exists(ConfigDir))
            {
                Directory.CreateDirectory(ConfigDir);
            }

            StreamWriter w = new StreamWriter(ConfigFile);
            w.WriteLine(DefaultConfig);
            w.Close();
        }

        public static void LoadConfig()
        {
            FileInfo f = new FileInfo(ConfigFile);
            
            if (f.Exists)
            {
                if (f.Extension.EndsWith("ini"))
                {
                    string[] lines = File.ReadAllLines(f.FullName);
                    if (lines[0] == "[SiCodeConfiguration]")
                    {
                        for (int i = 1; i < lines.Length; i++)
                        {
                            string[] lineSplitted = lines[i].Split('=');
                            string name = lineSplitted[0];
                            string value = lineSplitted[1];
                            switch (name)
                            {
                                case "EnableVisualEffects":
                                    EnableVisualEffects = Boolean.Parse(value);
                                    break;
                                case "EnableAutoSave":
                                    EnableAutoSave = Boolean.Parse(value);
                                    break;
                            }
                        }
                        Console.WriteLine("SiCode IDE configuration has been loaded!");
                    }
                    else
                    {
                        MessageBox.Show("Configuration read error, SiCode IDE will be stopped.", "SiCodeIDE.Configuration");
                        Environment.Exit(1);
                    }
                }
                else
                {
                    MessageBox.Show("Configuration read error, SiCode IDE will be stopped.", "SiCodeIDE.Configuration");
                    Environment.Exit(1);
                }
            }
            else
            {
                CreateDefaultConfig();
                LoadConfig();
            }
        } 
    }
}
