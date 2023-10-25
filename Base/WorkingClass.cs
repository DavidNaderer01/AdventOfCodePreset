using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePreset.Base
{
    public class WorkingClass<TOut, TIn> : AoCBase<TIn> 
        where TIn : IParsable<TIn> where TOut : IParsable<TOut>
    {
        public WorkingClass()
        {
            bool exampleTest = TestSolution(GetFile("input", ExampleEnding), GetFile("output", ExampleEnding));
            if(exampleTest)
            {
                string[] input = File.ReadAllLines(Prefix(InPath, GetFile("input", InputEnding)));
                string[] output = Solution(input);
                OutputGenerating(output);
            }
        }

        public override TIn Parse()
        {
            throw new NotImplementedException();
        }

        public override string[] Solution(string[] input)
        {
            throw new NotImplementedException();
        }
    }
}
