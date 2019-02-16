using System;
using System.Collections.Generic;
using System.Linq;

namespace Probability
{
    using SDU = StandardDiscreteUniform;
    // Weighted integer distribution using inverse cumulative method,
    // linear searching.
    public sealed class WeightedInteger : IDiscreteDistribution<int>
    {
        private readonly List<int> weights;
        private readonly List<int> cumulative;

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
            this.cumulative = new List<int>(weights.Count);
            cumulative.Add(weights[0]);
            for (int i = 1; i < weights.Count; i += 1)
                cumulative.Add(cumulative[i - 1] + weights[i]);
        }

        public IEnumerable<int> Support() =>
          Enumerable.Range(0, weights.Count).Where(x => weights[x] != 0);

        public int Weight(int i) =>
            0 <= i && i < weights.Count ? weights[i] : 0;

        public int Sample()
        {
            int total = cumulative[cumulative.Count - 1];
            int uniform = SDU.Distribution(1, total).Sample();
            int result = 0;
            while (cumulative[result] < uniform)
                result += 1;
            return result;
        }

        public override string ToString() =>
            $"WeightedInteger[{weights.CommaSeparated()}]";
    }
}
