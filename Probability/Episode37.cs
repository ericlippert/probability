using System;
using static System.Math;
namespace Probability
{
    static class Episode37
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 37 -- Stretching and shifting");
            ChangeTheStandardDeviation();
        }

        static void ChangeTheStandardDeviation()
        {
            // We can stretch a distribution by changing the standard deviation.
            var p = Normal.Distribution(0.75, 0.09);
            var p2 = Normal.Distribution(0.75, 0.15);
            double f(double x) => Atan(1000 * (x - .45)) * 20 - 31.2;
            for (int i = 0; i < 10; ++i)
                Console.WriteLine(
                  $"{p.ExpectedValueByImportance(f, 1.0, p2):0.###}");
        }
    }
}
