using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePreset.Base
{
    public interface IAoC<TIn>
    {
        public TIn Parse();
        public string[] Solution(string[] input);
    }
}
