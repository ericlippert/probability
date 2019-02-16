using System;
using System.Collections.Generic;
using System.Linq;

namespace Probability
{
    using SDU = StandardDiscreteUniform;
    // Weighted integer distribution using rejection sampling method.
    public sealed class WeightedInteger : IDiscreteDistribution<int>
    {
        private readonly List<int> weights;
        private readonly List<IDistribution<int>> distributions;

        public static IDiscreteDistribution<int> Distribution(params int[] weights) =>
            Distribution((IEnumerable<int>)weights);

        public static IDiscreteDistribution<int> Distribution(IEnumerable<int> weights)
        {
            List<int> w = weights.ToList();
            if (w.Any(x => x < 0) || !w.Any(x => x > 0))
                throw new ArgumentException();
            if (w.Count == 1)
                return Singleton<int>.Distribution(0);
            if (w.Count == 2)
                return Bernoulli.Distribution(w[0], w[1]);
            return new WeightedInteger(w);
        }


        private WeightedInteger(List<int> weights)
        {
            this.weights = weights;
            this.distributions =
              new List<IDistribution<int>>(weights.Count);
            int max = weights.Max();
            foreach (int w in weights)
                distributions.Add(Bernoulli.Distribution(w, max - w));
        }

        public IEnumerable<int> Support() =>
          Enumerable.Range(0, weights.Count).Where(x => weights[x] != 0);

        public int Weight(int i) =>
            0 <= i && i < weights.Count ? weights[i] : 0;

        public int Sample()
        {
            var rows = SDU.Distribution(0, weights.Count - 1);
            while (true)
            {
                int row = rows.Sample();
                if (distributions[row].Sample() == 0)
                    return row;
            }
        }

        public override string ToString() =>
            $"WeightedInteger[{weights.CommaSeparated()}]";
    }
}
