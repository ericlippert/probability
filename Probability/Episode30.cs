using System;

namespace Probability
{
    static class Episode30
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 30 -- Continuous prior");

            Console.WriteLine("Beta(5, 5) as the prior");
            var prior = Beta.Distribution(5, 5);
            Console.WriteLine(prior.Histogram(0, 1));
        }
    }
}