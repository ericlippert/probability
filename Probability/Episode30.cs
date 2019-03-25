using System;

namespace Probability
{
    using static Result;
    static class Episode30
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 30 -- Continuous prior");

            Console.WriteLine("Beta(5, 5) as the prior");
            var prior = Beta.Distribution(5, 5);
            Console.WriteLine(prior.Histogram(0, 1));

            IWeightedDistribution<Result> likelihood(double d) => 
                Flip<Result>.Distribution(Heads, Tails, d);

            /*
            IWeightedDistribution<double> posterior(Result r) =>
                Metropolis<double>.Distribution(
                    d => prior.Weight(d) * likelihood(d).Weight(r),
                    prior,
                    d => Normal.Distribution(d, 1));

            Console.WriteLine(posterior(Heads).Histogram(0, 1));
            */

            var posterior = prior.Posterior(likelihood);
            Console.WriteLine(posterior(Heads).Histogram(0, 1));           
        }
    }
}