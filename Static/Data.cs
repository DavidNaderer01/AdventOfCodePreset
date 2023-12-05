using AdventOfCodePreset.Base;

namespace AdventOfCodePreset.Static
{
    public class Data
    {
        public readonly string OutPath = GetCurrentDirectory() + "\\output";
        public readonly string InPath = GetCurrentDirectory() + "\\input";
        public const string OUTPUT_ENDING = "out";
        public const string INPUT_ENDING = "in";
        public const string SAMPLE_NAME = "sample";
        public const string ORIGINAL_NAME = "origin";

        private static string GetCurrentDirectory()
        {
            DirectoryInfo currentDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            return currentDirectory.Parent.Parent.Parent.FullName;
        }
    }
}
