using System;
using System.Collections.Generic;
using System.Linq;

namespace Probability
{
    using SDU = StandardDiscreteUniform;
    // Weighted integer distribution using alias method.
    public sealed class WeightedInteger : IDiscreteDistribution<int>, IWeightedDistribution<int>
    {
        private readonly List<int> weights;
        private readonly IDistribution<int>[] distributions;

        public static IDiscreteDistribution<int> Distribution(params int[] weights) =>
            Distribution((IEnumerable<int>)weights);

        public static IDiscreteDistribution<int> Distribution(IEnumerable<int> weights)
        {
            List<int> w = weights.ToList();
            if (w.Any(x => x < 0))
                throw new ArgumentException();
            if (!w.Any(x => x > 0))
                return Empty<int>.Distribution;
            if (w.Count == 1)
                return Singleton<int>.Distribution(0);
            if (w.Count == 2)
                return Bernoulli.Distribution(w[0], w[1]);
            int gcd = weights.GCD();
            for (int i = 0; i < w.Count; i += 1)
                w[i] /= gcd;
            return new WeightedInteger(w);
        }

        private WeightedInteger(IEnumerable<int> weights)
        {
            this.weights = weights.ToList();
            int s = this.weights.Sum();
            int n = this.weights.Count;
            this.distributions = new IDistribution<int>[n];
            var lows = new Dictionary<int, int>();
            var highs = new Dictionary<int, int>();
            for (int i = 0; i < n; i += 1)
            {
                int w = this.weights[i] * n;
                if (w == s)
                    this.distributions[i] = Singleton<int>.Distribution(i);
                else if (w < s)
                    lows.Add(i, w);
                else
                    highs.Add(i, w);
            }
            while (lows.Any())
            {
                var low = lows.First();
                lows.Remove(low.Key);
                var high = highs.First();
                highs.Remove(high.Key);
                int lowNeeds = s - low.Value;
                this.distributions[low.Key] =
                    Bernoulli.Distribution(low.Value, lowNeeds)
                      .Select(x => x == 0 ? low.Key : high.Key);
                int newHigh = high.Value - lowNeeds;
                if (newHigh == s)
                    this.distributions[high.Key] =
                      Singleton<int>.Distribution(high.Key);
                else if (newHigh < s)
                    lows[high.Key] = newHigh;
                else
                    highs[high.Key] = newHigh;
            }
        }

        public IEnumerable<int> Support() =>
          Enumerable.Range(0, weights.Count).Where(x => weights[x] != 0);

        public int Weight(int i) =>
            0 <= i && i < weights.Count ? weights[i] : 0;

        double IWeightedDistribution<int>.Weight(int t) => this.Weight(t);

        public int Sample()
        {
            int i = SDU.Distribution(0, weights.Count - 1).Sample();
            return distributions[i].Sample();
        }

        public override string ToString() =>
            $"WeightedInteger[{weights.CommaSeparated()}]";
    }
}
