using System;
using System.Collections.Generic;
using System.Linq;

namespace Probability
{
    public sealed class Projected<A, R> : IDiscreteDistribution<R>
    {
        private readonly IDiscreteDistribution<A> underlying;
        private readonly Func<A, R> projection;
        private readonly Dictionary<R, int> weights;
        public static IDiscreteDistribution<R> Distribution(
          IDiscreteDistribution<A> underlying,
          Func<A, R> projection)
        {
            var result = new Projected<A, R>(underlying, projection);
            if (!result.Support().Any())
                return Empty<R>.Distribution;
            if (result.Support().Count() == 1)
                return Singleton<R>.Distribution(result.Support().First());
            return result;
        }
        private Projected(
          IDiscreteDistribution<A> underlying,
          Func<A, R> projection)
        {
            this.underlying = underlying;
            this.projection = projection;
            this.weights = underlying.Support().
              GroupBy(
                projection,
                a => underlying.Weight(a)).
              ToDictionary(g => g.Key, g => g.Sum());
        }
        public R Sample() => projection(underlying.Sample());
        public IEnumerable<R> Support() => this.weights.Keys;
        public int Weight(R r) =>
          this.weights.GetValueOrDefault(r, 0);
    }
}
