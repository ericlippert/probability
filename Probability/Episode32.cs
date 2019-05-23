using System;
using static System.Math;
namespace Probability
{
    static class Episode32
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 32 -- Black swan attack");

            var p = Normal.Distribution(0.75, 0.09);
            double f(double x) => Atan(1000 * (x - .45)) * 20 - 31.2;
            for (int i = 0; i < 100; ++i)
                Console.WriteLine($"{p.ExpectedValueBySampling(f):0.##}");
        }
    }
}
