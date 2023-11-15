using HCI_Projekat_1.Models;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HCI_Projekat_1.Util
{
    class ThemeUtil
    {
        private static readonly String PATH = "Themes";

        public static List<Theme> GetThemes()
        {
            string executablePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(executablePath, @"..\..\.."));
            string themesFolderPath = Path.Combine(projectPath, "Themes");
            var files = Directory.GetFiles(themesFolderPath);
            var list = new List<Theme>();
            foreach (var file in files)
            {
                var name = Path.GetFileNameWithoutExtension(file);
                Theme theme = new Theme { Name = name, Path = file };
                list.Add(theme);
            }
            return list;
        }

        public static void ChangeTheme(Uri uri)
        {
            ResourceDictionary resourceDictionary = new ResourceDictionary { Source = uri };
            foreach (DictionaryEntry entry in resourceDictionary)
                App.Current.Resources[entry.Key] = entry.Value;
        }
    }
}
