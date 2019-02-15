using System;
using System.Linq;
namespace Probability
{
    using SDU = StandardDiscreteUniform;
    static class Episode04
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 04");
            Console.WriteLine("10d6:");
            Console.WriteLine(SDU.Distribution(1, 6).Samples().Take(10).Sum());
            Console.WriteLine("1d10:");
            Console.WriteLine(SDU.Distribution(1, 10).Histogram());
            Console.WriteLine("1d6:");
            Console.WriteLine(SDU.Distribution(1, 6).ShowWeights());
        }
    }
}
