using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePreset.Base
{
    public class HelpBasic
    {
        public string Prefix(string path, string file) 
            => $"{path}\\{file}";
        public string GetFile(string fileName, string fileExtension)
            => $"{fileName}.{fileExtension}";

    }
}
