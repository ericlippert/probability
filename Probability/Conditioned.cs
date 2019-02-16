using System;
using System.Collections.Generic;
using System.Linq;

namespace Probability
{
    // Conditioned distribution using rejection sampling
    public sealed class Conditioned<T> :
      IDiscreteDistribution<T>
    {
        private readonly List<T> support;
        private readonly IDiscreteDistribution<T> underlying;
        private readonly Func<T, bool> predicate;

        public static IDiscreteDistribution<T> Distribution(
          IDiscreteDistribution<T> underlying,
          Func<T, bool> predicate)
        {
            var d = new Conditioned<T>(underlying, predicate);
            if (d.support.Count == 0)
                throw new ArgumentException();
            if (d.support.Count == 1)
                return Singleton<T>.Distribution(d.support[0]);
            return d;
        }

        private Conditioned(
          IDiscreteDistribution<T> underlying,
          Func<T, bool> predicate)
        {
            this.underlying = underlying;
            this.predicate = predicate;
            this.support = underlying.Support()
              .Where(predicate)
              .ToList();
        }

        public T Sample()
        {
            while (true)
            {
                T t = this.underlying.Sample();
                if (this.predicate(t))
                    return t;
            }
        }

        public IEnumerable<T> Support() => this.support.Select(x => x);

        public int Weight(T t) => predicate(t) ? underlying.Weight(t) : 0;
    }
}
