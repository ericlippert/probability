using System.Collections.Generic;
using System.Linq;
namespace Probability
{
    public sealed class MarkovBuilder<T>
    {
        private DistributionBuilder<T> initial =
          new DistributionBuilder<T>();
        private Dictionary<T, DistributionBuilder<T>> transitions =
          new Dictionary<T, DistributionBuilder<T>>();

        public void AddInitial(T t)
        {
            initial.Add(t);
        }

        public void AddTransition(T t1, T t2)
        {
            if (!transitions.ContainsKey(t1))
                transitions[t1] = new DistributionBuilder<T>();
            transitions[t1].Add(t2);
        }

        public Markov<T> ToDistribution()
        {
            var init = initial.ToDistribution();
            var trans = transitions.ToDictionary(
              kv => kv.Key,
              kv => kv.Value.ToDistribution());
            return Markov<T>.Distribution(
              init,
              k => trans.GetValueOrDefault(k, Empty<T>.Distribution));
        }
    }
}
