using System.Collections.Generic;
namespace Probability
{
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
    }
}
