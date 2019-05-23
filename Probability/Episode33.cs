using System;
using static System.Math;
namespace Probability
{
    static class Episode33
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 33 -- Expected value by quadrature");

            var p = Normal.Distribution(0.75, 0.09);
            double f(double x) => Atan(1000 * (x - .45)) * 20 - 31.2;
            Console.WriteLine(p.ExpectedValueByQuadrature(f));
        }
    }
}
