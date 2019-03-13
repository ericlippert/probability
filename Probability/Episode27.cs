using System;
using static System.Math;

namespace Probability
{
    static class Episode27
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 27 -- Rejection sampling redux");

            double Mixture(double x) =>
                Exp(-x * x) + Exp((1.0 - x) * (x - 1.0) * 10.0);

            var r = Rejection<double>.Distribution(Mixture, Normal.Standard, 7.0);
            Console.WriteLine(r.Histogram(-2.0, 2.0));
        }
    }
}
