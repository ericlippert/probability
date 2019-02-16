using System;
using System.Collections.Generic;

enum Cold { No, Yes }
enum Sneezed { No, Yes }

namespace Probability
{
    static class Episode12
    {
        static IDiscreteDistribution<Sneezed> SneezedGivenCold(Cold c)
        {
            var list = new List<Sneezed>() { Sneezed.No, Sneezed.Yes };
            return c == Cold.No ?
              list.ToWeighted(97, 3) :
              list.ToWeighted(15, 85);
        }

        public static void DoIt()
        {
            Console.WriteLine("Episode 12");

            var colds = new List<Cold>() { Cold.No, Cold.Yes };
            IDiscreteDistribution<Cold> cold = colds.ToWeighted(90, 10);
            Console.WriteLine("Combined distribution: prior probability of having a cold");
            Console.WriteLine("conditional probability of having sneezed");
            Console.WriteLine(cold.SelectMany(SneezedGivenCold).Histogram());
        }
    }
}
