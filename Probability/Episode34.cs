using System;
using static System.Math;
namespace Probability
{
    static class Episode34
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 34 -- Importance sampling with SCUD");

            var p = Normal.Distribution(0.75, 0.09);
            double f(double x) => Atan(1000 * (x - .45)) * 20 - 31.2;
            var u = StandardContinuousUniform.Distribution;
            double np = 1.0; // p is normalized
            double w(double x) => f(x) * p.Weight(x) / np;
            for (int i = 0; i < 100; ++i)
                Console.WriteLine($"{u.ExpectedValueBySampling(w):0.###}");
        }
    }
}
