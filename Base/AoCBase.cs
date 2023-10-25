using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePreset.Base
{
    public abstract class AoCBase<TIn> : HelpBasic, IAoC<TIn> 
        where TIn : IParsable<TIn>
    {
        protected string InPath = Directory.GetCurrentDirectory() + "\\input";
        protected string OutPath = Directory.GetCurrentDirectory() + "\\output";
        protected string ExampleEnding = "ex";
        protected string InputEnding = "in";
        protected string OutputEnding = "out";
        protected AoCBase()
        {
            CreateFolder(InPath);
            CreateFolder(OutPath);
        }

        public abstract TIn Parse();

        public abstract string[] Solution(string[] input);

        public bool TestSolution(string file, string exeptedFile)
        {
            string[] input = File.ReadAllLines(Prefix(InPath, file));
            string[] value = Solution(input);
            string[] expected = File.ReadAllLines(Prefix(OutPath, file));
            return CheckAnswer(value, expected);
        }
        public void OutputGenerating(string[] input)
        {
            try
            {
                File.WriteAllLines(Prefix(OutPath, GetFile("output", OutputEnding)), input);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private bool CheckAnswer(string[] value, string[] answer)
        {
            bool solution = value[0] == answer[0] 
                & value.Length != answer.Length;
            for(int i = 1; i < answer.Length & solution; i++)
            {
                solution = value[i] == answer[i];
            }
            return solution;
        }
        private void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
