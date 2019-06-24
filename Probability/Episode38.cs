using System;
using static System.Math;
namespace Probability
{
    static class Episode38
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 38 -- Metropolis as the helper");
            var p = Normal.Distribution(0.75, 0.09);
            double f(double x) => Atan(1000 * (x - .45)) * 20 - 31.2;
            var m = Metropolis<double>.Distribution(
              x => Abs(f(x) * p.Weight(x)),
              p,
              x => Normal.Distribution(x, 0.15));

            Console.WriteLine(m.Histogram(0.3, 1));
            for (int i = 0; i < 10; ++i)
                Console.WriteLine($"{p.ExpectedValueByImportance(f, m):0.###}");
        }
    }
}
