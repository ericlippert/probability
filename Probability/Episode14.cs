using System;
using System.Collections.Generic;

enum Height { Tall, Medium, Short }
enum Severity { Severe, Moderate, Mild }
enum Prescription { DoubleDose, NormalDose, HalfDose }

namespace Probability
{
    using static Height;
    using static Severity;
    using static Prescription;

    static class Episode14
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 14");
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
