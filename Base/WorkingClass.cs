using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePreset.Base
{
    /// <summary>
    /// Create a instance for a AoC solution
    /// </summary>
    /// <typeparam name="TOut">Output type</typeparam>
    /// <typeparam name="TIn">Input type</typeparam>
    public class WorkingClass<TOut, TIn> : AoCBase<TIn> 
    {
        private readonly Func<string[], TIn> _parser;
        private readonly Func<TIn, string[]> _solution;
        public WorkingClass(Func<string[], TIn> parser, Func<TIn, string[]> solution)
            : base()
        {
            _parser = parser;
            _solution = solution;
            bool exampleTest = TestSolution(GetFile(SAMPLE_NAME, INPUT_ENDING), GetFile(SAMPLE_NAME, OUTPUT_ENDING));
            if (exampleTest)
            {
                string[] input = File.ReadAllLines(Prefix(InPath, GetFile(ORIGINAL_NAME, INPUT_ENDING)));
                string[] output = Solution(input);
                OutputGenerating(output);
            }
            Console.Write($"Output:{exampleTest}");
        }

        public override TIn Parse(string[] input)
        {
            return _parser(input);
        }

        public override string[] Solution(string[] input)
        {
            TIn parsedInput = Parse(input);
            return _solution(parsedInput);
        }
    }
}
