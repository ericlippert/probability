using System;
using static System.Math;

namespace Probability
{
    static class Episode28
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 28 -- Metropolis");

            double Mixture(double x) =>
                Exp(-x * x) + Exp((1.0 - x) * (x - 1.0) * 10.0);

            Console.WriteLine(Distribution.NormalMetropolis(Mixture).Histogram(-2, 2));
        }
    }
}
