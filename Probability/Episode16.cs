using System;
using System.Collections.Generic;

enum TappetsDisease { Sick, Healthy }
enum JethroTest { Positive, Negative }

namespace Probability
{
    using static Height;
    using static Severity;
    using static Prescription;
    using static TappetsDisease;
    using static JethroTest;

    static class Episode16
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 16");
            ComputeHeightPosterior();
            ComputeTestAccuracy();
        }

        private static void ComputeHeightPosterior()
        { 
            var prior = new List<Height>() { Tall, Medium, Short }.ToWeighted(5, 2, 1);

            var severity = new List<Severity> { Severe, Moderate, Mild };

            IDiscreteDistribution<Severity> likelihood(Height h)
            {
                switch (h)
                {
                    case Tall:
                        return severity.ToWeighted(10, 11, 0);
                    case Medium:
                        return severity.ToWeighted(0, 12, 5);
                    default:
                        return severity.ToWeighted(0, 0, 1);
                }
            }

            Console.WriteLine("Joint distribution of height and severity:");
            var joint = prior.Joint(likelihood);
            Console.WriteLine(joint.Histogram());
            Console.WriteLine(joint.ShowWeights());

            Console.WriteLine("Posterior distribution of height given moderate symptoms:");
            var posterior = prior.Posterior(likelihood);
            Console.WriteLine(posterior(Moderate).ShowWeights());

        }

        static void ComputeTestAccuracy()
        {
            var prior = new List<TappetsDisease> { Sick, Healthy }.ToWeighted(1, 99);

            var tests = new List<JethroTest> { Positive, Negative };
            IDiscreteDistribution<JethroTest> likelihood(TappetsDisease d) =>
                d == Sick ? 
                    tests.ToWeighted(99, 1) : 
                    tests.ToWeighted(1, 99);

            Console.WriteLine("Joint distribution of sickness and test results");
            Console.WriteLine(prior.Joint(likelihood).ShowWeights());
            Console.WriteLine("Posterior distribution of sickness given positive test:");
            var posterior = prior.Posterior(likelihood);
            Console.WriteLine(posterior(Positive).ShowWeights());
            Console.WriteLine("Posterior distribution of sickness given negative test:");
            Console.WriteLine(posterior(Negative).ShowWeights());
        }
    }
}
