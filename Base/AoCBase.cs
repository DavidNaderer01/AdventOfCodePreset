using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodePreset.Base
{
    public abstract class AoCBase<TIn> : HelpBasic, IAoC<TIn>
    {
        protected AoCBase()
        {
            CreateFolder(InPath);
            CreateFolder(OutPath);
        }

        public abstract TIn Parse(string[] input);

        public abstract string[] Solution(string[] input);

        public bool TestSolution(string file, string exeptedFile)
        {
            if (TryReadFile(Prefix(InPath, file), out string[] input))
            {
                string[] value = Solution(input);
                string[] expected = File.ReadAllLines(Prefix(OutPath, exeptedFile));
                return CheckAnswer(value, expected);
            }
            Console.Write($"Create {file}");
            return false;
        }
        public void OutputGenerating(string[] input)
        {
            File.WriteAllLines(Prefix(OutPath, GetFile(ORIGINAL_NAME, OUTPUT_ENDING)), input);
            foreach(string line in input)
                Console.WriteLine(line);
        }
        private bool CheckAnswer(string[] value, string[] answer)
        {
            bool solution = value[0] == answer[0]
                & value.Length == answer.Length;
            for (int i = 1; i < answer.Length & solution; i++)
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
