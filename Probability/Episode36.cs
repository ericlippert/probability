using System;
using static System.Math;
namespace Probability
{
    static class Episode36
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 36 -- Importance sampling for real");

            var p = Normal.Distribution(0.75, 0.09);
            double f(double x) => Atan(1000 * (x - .45)) * 20 - 31.2;
            var u = StandardContinuousUniform.Distribution;
            Console.WriteLine("Estimate the ratio");
            for (int i = 0; i < 10; ++i)
                Console.WriteLine($"{p.ExpectedValueByImportance(f, u):0.###}");
            Console.WriteLine("Known ratio");
            for (int i = 0; i < 10; ++i)
                Console.WriteLine($"{p.ExpectedValueByImportance(f, 1.0, u):0.###}");
        }
    }
}
