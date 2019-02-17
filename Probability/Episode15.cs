using System;
using System.Collections.Generic;

namespace Probability
{
    using static Height;
    using static Severity;
    using static Prescription;

    static class Episode15
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 15");
            var prior = new List<Height>() { Tall, Medium, Short }.ToWeighted(60, 30, 10);

            var severity = new List<Severity> { Severe, Moderate, Mild };

            IDiscreteDistribution<Severity> likelihood(Height h)
            {
                switch (h)
                {
                    case Tall: 
                        return severity.ToWeighted(45, 55, 0);
                    case Medium: 
                        return severity.ToWeighted(0, 70, 30);
                    default: 
                        return severity.ToWeighted(0, 0, 1);
                }
            }

            Prescription projection(Height h, Severity s)
            {
                switch (h)
                {
                    case Tall: return s == Severe ? DoubleDose : NormalDose;
                    case Medium: return s == Mild ? HalfDose : NormalDose;
                    default: return HalfDose;
                }
            }

            Console.WriteLine("Joint distribution of height and severity:");
            var joint = prior.Joint(likelihood);
            Console.WriteLine(joint.Histogram());
            Console.WriteLine(joint.ShowWeights());

            Console.WriteLine("Distribution of doses:");
            var doses = prior.SelectMany(likelihood, projection);
            Console.WriteLine(doses.Histogram());
            Console.WriteLine(doses.ShowWeights());
        }
    }
}
