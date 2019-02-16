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
                Func<A, R> projection) =>
            Projected<A, R>.Distribution(d, projection);

        public static IDiscreteDistribution<T> Where<T>(
                this IDiscreteDistribution<T> d,    
                Func<T, bool> predicate) =>
            Conditioned<T>.Distribution(d, predicate);

        public static IDiscreteDistribution<T> ToUniform<T>(
            this IEnumerable<T> items)
        {
            var list = items.ToList();
            return SDU.Distribution(0, list.Count - 1)
                .Select(i => list[i]);
        }
    }
}
