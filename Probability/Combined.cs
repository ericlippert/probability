using System;
using System.Collections.Generic;
using System.Linq;

namespace Probability
{
    // Combined probability distribution (with a projection)
    public sealed class Combined<A, B, C> :
      IDiscreteDistribution<C>
    {

        private readonly List<C> support;
        private readonly IDiscreteDistribution<A> prior;
        private readonly Func<A, IDiscreteDistribution<B>> likelihood;
        private readonly Func<A, B, C> projection;

        public static IDiscreteDistribution<C> Distribution(
                IDiscreteDistribution<A> prior,
                Func<A, IDiscreteDistribution<B>> likelihood,
                Func<A, B, C> projection) =>
            new Combined<A, B, C>(prior, likelihood, projection);

        private Combined(
            IDiscreteDistribution<A> prior,
            Func<A, IDiscreteDistribution<B>> likelihood,
            Func<A, B, C> projection)
        {
            this.prior = prior;
            this.likelihood = likelihood;
            this.projection = projection;
            var s = from a in prior.Support()
                    from b in this.likelihood(a).Support()
                    select projection(a, b);
            this.support = s.Distinct().ToList();
        }

        public IEnumerable<C> Support() =>
            this.support.Select(x => x);

        public C Sample()
        {
            A a = this.prior.Sample();
            B b = this.likelihood(a).Sample();
            return this.projection(a, b);
        }

        public int Weight(C c) => 
            throw new NotImplementedException();
    }
}
