using System;
using System.Threading;

namespace Probability
{
    // A threadsafe, all-static, crypto-randomized wrapper around Random.
    // Still not great, but a slight improvement.
    public static class Pseudorandom
    {
        private readonly static ThreadLocal<Random> prng =
          new ThreadLocal<Random>(() =>
            new Random(BetterRandom.NextInt()));
        public static int NextInt() => prng.Value.Next();
        public static double NextDouble() => prng.Value.NextDouble();
    }
}