using System;
using System.Linq;
namespace Probability
{
    static class Episode24
    {
        static IDiscreteDistribution<bool> Flip() =>
            Bernoulli.Distribution(1, 1).Select(x => x == 0);

        public static void DoIt()
        {
            Console.WriteLine("Episode 24 - Gamblers Ruin Markov Chain generation");
            const int n = 5;

            var initial = Singleton<int>.Distribution(n);

            IDistribution<int> transition(int x) =>
              (x == 0 || x == 2 * n) ?
                Empty<int>.Distribution :
                Flip().Select(b => b ? x - 1 : x + 1);

            var markov = Markov<int>.Distribution(initial, transition);
            Console.WriteLine("Sample game");
            Console.WriteLine(markov.Sample().CommaSeparated());
            Console.WriteLine("Histogram of game lengths");
            Console.WriteLine(markov.Samples().Select(x => x.Count()).DiscreteHistogram());
        }
    }
}
