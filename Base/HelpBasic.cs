using AdventOfCodePreset.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePreset.Base
{
    public class HelpBasic : Data
    {
        public string Prefix(string path, string file) 
            => $"{path}\\{file}";
        public string GetFile(string fileName, string fileExtension)
            => $"{fileName}.{fileExtension}";
        public bool TryReadFile(string path, out string[] lines)
        {
            lines = default!;
            bool exists = File.Exists(path);
            if(exists)
            {
                lines = File.ReadAllLines(path);
            }
            return exists;
        }
    }
}
