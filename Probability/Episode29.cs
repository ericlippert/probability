using System;

namespace Probability
{
    using static Coin;
    using static Result;

    enum Coin { Fair, DoubleHeaded }

    enum Result { Heads, Tails }

    static class Episode29
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 29 -- Rosencrantz flips a coin");
            // 999 fair coins, one double-headed coin :
            var coins = new[] { Fair, DoubleHeaded };
            var results = new[] { Heads, Tails };
            var prior = coins.ToWeighted(999, 1);
            IDiscreteDistribution<Result> Likelihood(Coin c) =>
                results.ToWeighted(1, c == Fair ? 1 : 0);
            var c1 = from c in prior
                     from r in Likelihood(c)
                     where r == Heads
                     select c;
            Console.WriteLine(c1.ShowWeights());
            var c2 = from c in prior
                     from r1 in Likelihood(c)
                     where r1 == Heads
                     from r2 in Likelihood(c)
                     where r2 == Heads
                     select c;
            Console.WriteLine(c2.ShowWeights());
        }
    }
}