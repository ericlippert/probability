using System;
namespace Probability
{
    static class Episode05
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 05");
            Console.WriteLine("Bernoulli 75% chance of 1");
            Console.WriteLine(Bernoulli.Distribution(1, 3).Histogram());
        }
    }
}
