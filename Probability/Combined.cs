using System;
using System.Collections.Generic;
using System.Linq;

namespace Probability
{
    public sealed class Combined<A, R> : IDiscreteDistribution<R>
    {
        private readonly List<R> support;
        private readonly IDiscreteDistribution<A> prior;
        private readonly Func<A, IDiscreteDistribution<R>> likelihood;

        public static IDiscreteDistribution<R> Distribution(
                IDiscreteDistribution<A> prior,
                Func<A, IDiscreteDistribution<R>> likelihood) =>
            new Combined<A, R>(prior, likelihood);

        private Combined(
              IDiscreteDistribution<A> prior,
              Func<A, IDiscreteDistribution<R>> likelihood)
        {
            this.prior = prior;
            this.likelihood = likelihood;
            var q = from a in prior.Support()
                    from b in this.likelihood(a).Support()
                    select b;
            this.support = q.Distinct().ToList();
        }
        public IEnumerable<R> Support() =>
            this.support.Select(x => x);  

        public R Sample() =>
            this.likelihood(this.prior.Sample()).Sample();

        public int Weight(R r) => 
            throw new NotImplementedException();
    }
}
