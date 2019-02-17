using System;
using System.Linq;
using System.Collections.Generic;
namespace Probability
{
    using SDU = StandardDiscreteUniform;
    // Extension methods on distributions
    public static class Distribution
    {
        public static IEnumerable<T> Samples<T>(
          this IDistribution<T> d)
        {
            while (true)
                yield return d.Sample();
        }

        public static string Histogram(
            this IDistribution<double> d, double low, double high) =>
            d.Samples().Histogram(low, high);

        public static string Histogram<T>(this IDiscreteDistribution<T> d) =>
            d.Samples().DiscreteHistogram();

        public static string ShowWeights<T>(this IDiscreteDistribution<T> d)
        {
            int labelMax = d.Support()
                .Select(x => x.ToString().Length)
                .Max();
            return d.Support()
                .Select(s => $"{ToLabel(s)}:{d.Weight(s)}")
                .NewlineSeparated();
            string ToLabel(T t) =>
                t.ToString().PadLeft(labelMax);
        }

        public static IDiscreteDistribution<R> Select<A, R>(
            this IDiscreteDistribution<A> d,
            Func<A, R> projection)
        {
            var dict = d.Support()
                .GroupBy(projection, a => d.Weight(a))
                .ToDictionary(g => g.Key, g => g.Sum());
            var rs = dict.Keys.ToList();
            return Projected<int, R>.Distribution(
                WeightedInteger.Distribution(rs.Select(r => dict[r])),
                i => rs[i]);
        }

        public static IDiscreteDistribution<T> Where<T>(
            this IDiscreteDistribution<T> d,
            Func<T, bool> predicate)
        {
            var s = d.Support().Where(predicate).ToList();
            return s.ToWeighted(s.Select(t => d.Weight(t)));
        }

        public static IDiscreteDistribution<C> SelectMany<A, B, C>(
            this IDiscreteDistribution<A> prior,
            Func<A, IDiscreteDistribution<B>> likelihood,
            Func<A, B, C> projection)
        {
            int product = prior.Support()
                .Select(a => likelihood(a).TotalWeight())
                .Where(x => x != 0)
                .Product();
            var w = from a in prior.Support()
                    let pb = likelihood(a)
                    from b in pb.Support()
                    group prior.Weight(a) * pb.Weight(b) * product / pb.TotalWeight()
                    by projection(a, b);
            var dict = w.ToDictionary(g => g.Key, g => g.Sum());
            return dict.Keys.ToWeighted(dict.Values);
        }

        public static IDiscreteDistribution<B> SelectMany<A, B>(
                this IDiscreteDistribution<A> prior,
                Func<A, IDiscreteDistribution<B>> likelihood) =>
            SelectMany(prior, likelihood, (a, b) => b);

        public static IDiscreteDistribution<(A, B)> Joint<A, B>(
                this IDiscreteDistribution<A> prior,
                Func<A, IDiscreteDistribution<B>> likelihood) =>
            SelectMany(prior, likelihood, (a, b) => (a, b));


        public static Func<B, IDiscreteDistribution<C>> Posterior<A, B, C>(
                this IDiscreteDistribution<A> prior,
                Func<A, IDiscreteDistribution<B>> likelihood,
                Func<A, B, C> projection) =>
          b => from a in prior
               from bb in likelihood(a)
               where object.Equals(b, bb)
               select projection(a, b);

        public static Func<B, IDiscreteDistribution<A>> Posterior<A, B>(
                this IDiscreteDistribution<A> prior,
                Func<A, IDiscreteDistribution<B>> likelihood) =>
            Posterior(prior, likelihood, (a, b) => a);

        public static IDiscreteDistribution<T> ToUniform<T>(
            this IEnumerable<T> items)
        {
            var list = items.ToList();
            return SDU.Distribution(0, list.Count - 1)
                .Select(i => list[i]);
        }

        public static IDiscreteDistribution<T> ToWeighted<T>(
            this IEnumerable<T> items,
            IEnumerable<int> weights)
        {
            var list = items.ToList();
            return WeightedInteger.Distribution(weights).Select(i => list[i]);
        }

        public static IDiscreteDistribution<T> ToWeighted<T>(
                this IEnumerable<T> items,
                params int[] weights) =>
            items.ToWeighted((IEnumerable<int>)weights);

        public static int TotalWeight<T>(this IDiscreteDistribution<T> d) =>
            d.Support().Select(t => d.Weight(t)).Sum();

        public static double ExpectedValue(this IDiscreteDistribution<int> d) =>
            d.Support()
            .Select(s => 
                (double)s * d.Weight(s)).Sum() / d.TotalWeight();
    }
}
