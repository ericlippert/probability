using System;
using static System.Math;
namespace Probability
{
    static class Bonus02
    {
        public static void DoIt()
        {
            Console.WriteLine("Bonus 2 -- Noisy Or");
            // 1 is a pigeon *failed* to get from Alpha to Bravo
            var ab = Bernoulli.Distribution(95, 5);
            // 1 is a pigeon *failed* to get from Bravo to Charlie
            var bc = Bernoulli.Distribution(96, 4);
            // 1 is a pigeon was *not* observed
            var d = Bernoulli.Distribution(98, 2);
            // Delta reports that the channel is unhealthy if
            // there is no pigeon between A and B, OR
            // there is no pigeon between B and C, OR
            // delta failed to observe a pigeon between A and B, OR
            // delta failed to observe a pigeon between B and C,
            // so report is 1 if delta reports the channel is
            // *unhealthy*.
            var result = from pab in ab
                         from pbc in bc
                         from oab in d
                         from obc in d
                         let report = pab | pbc | oab | obc
                         where report == 1
                         select (pab, pbc);
            Console.WriteLine(result.ShowWeights());

        }
    }
}
