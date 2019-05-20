using System;

namespace Probability
{
    using static Result;
    static class Episode31
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 31 -- Expected value refresher");
            Console.WriteLine("Expected fairness of coin drawn from Beta(2, 5)");
            var distribution = Beta.Distribution(2, 5);
            Console.WriteLine(distribution.Histogram(0, 1));
            Console.WriteLine(distribution.ExpectedValue());

            Console.WriteLine("Expected fairness of coin from episode 30");
            var prior = Beta.Distribution(5, 5);
            IWeightedDistribution<Result> likelihood(double d) =>
                Flip<Result>.Distribution(Heads, Tails, d);
            var posterior = prior.Posterior(likelihood)(Heads);
            Console.WriteLine(posterior.Histogram(0, 1));
            Console.WriteLine(posterior.ExpectedValue());
        }
    }
}