using AdventOfCodePreset.Base;
using AdventOfCodePreset.Static;

namespace AdventOfCodePreset
{
    /// <summary>
    /// Creates a worker instance and let it work.
    /// </summary>
    /// <typeparam name="TOut">Change it to the needed type</typeparam>
    /// <typeparam name="TIn">Change it to the needed type</typeparam>
    class Program<TOut, TIn>
    {
        public static void Main(string[] args) 
        {
            WorkingClass<TOut, TIn> worker = new(Parse, Solution);
        }

        private static TIn Parse(string[] input)
        {
            throw new NotImplementedException();
        }

        private static string[] Solution(TIn values)
        {
            throw new NotImplementedException();
        }
    }
}