using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace UiDesktopApp1
{
    internal class IniFile
    {
        private readonly Dictionary<string, Dictionary<string, string>> sections;

        public IniFile()
        {
            sections = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
        }

        public void Load(string filePath)
        {
            string currentSection = null;
            sections.Clear();

            foreach (string line in File.ReadLines(filePath))
            {
                string trimmedLine = line.Trim();

                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2);
                    sections[currentSection] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }
                else if (!string.IsNullOrEmpty(currentSection))
                {
                    int equalsIndex = trimmedLine.IndexOf('=');
                    if (equalsIndex >= 0)
                    {
                        string key = trimmedLine.Substring(0, equalsIndex).Trim();
                        string value = trimmedLine.Substring(equalsIndex + 1).Trim();
                        sections[currentSection][key] = value;
                    }
                }
            }
        }

        public string GetValue(string section, string key)
        {
            if (sections.TryGetValue(section, out Dictionary<string, string> sectionData))
            {
                if (sectionData.TryGetValue(key, out string value))
                {
                    return value;
                }
            }
            return null;
        }
    }
}
