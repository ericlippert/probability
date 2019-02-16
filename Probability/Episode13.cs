using System;
using System.Collections.Generic;

namespace Probability
{
    static class Episode13
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
            Console.WriteLine("Episode 13");

            var colds = new List<Cold>() { Cold.No, Cold.Yes };
            IDiscreteDistribution<Cold> cold = colds.ToWeighted(90, 10);

            var sneezed = from c in cold
                          from s in SneezedGivenCold(c)
                          select s;

            var joint = cold.Joint(SneezedGivenCold);

            Console.WriteLine(sneezed.Histogram());
            // Console.WriteLine(sneezed.ShowWeights());
            Console.WriteLine(joint.Histogram());
            // Console.WriteLine(joint.ShowWeights());
        }
    }
}
