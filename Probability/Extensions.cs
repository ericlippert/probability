using System.Collections.Generic;
using System.Linq;

namespace Probability
{
    // Miscellaneous extension methods
    static class Extensions
    {
        public static string Histogram(
            this IEnumerable<double> d, double low, double high)
        {
            const int width = 40;
            const int height = 20;
            const int sampleCount = 100000;
            int[] buckets = new int[width];
            foreach (double c in d.Take(sampleCount))
            {
                int bucket = (int)(buckets.Length * (c - low) / (high - low));
                if (0 <= bucket && bucket < buckets.Length)
                    buckets[bucket] += 1;
            }
            int max = buckets.Max();
            double scale =
                max < height ? 1.0 : ((double)height) / max;
            return Enumerable.Range(0, height)
                    .Select(r => buckets.Select(b => b * scale > (height - r) ? '*' : ' ').Concatenated() + "\n")
                    .Concatenated()
                    + new string('-', width) + "\n";
        }

        public static string DiscreteHistogram<T>(this IEnumerable<T> d)
        {
            const int sampleCount = 100000;
            const int width = 40;
            var dict = d.Take(sampleCount)
                .GroupBy(x => x)
                .ToDictionary(g => g.Key, g => g.Count());
            int labelMax = dict.Keys
                .Select(x => x.ToString().Length)
                .Max();
            var sup = dict.Keys.OrderBy(k => k).ToList();
            int max = dict.Values.Max();
            double scale = max < width ? 1.0 : ((double)width) / max;
            return sup.Select(s => $"{ToLabel(s)}|{Bar(s)}").NewlineSeparated();
            string ToLabel(T t) =>
                t.ToString().PadLeft(labelMax);
            string Bar(T t) =>
                new string('*', (int)(dict[t] * scale));
        }

        public static string Separated<T>(this IEnumerable<T> items, string s) =>
            string.Join(s, items);

        public static string Concatenated<T>(this IEnumerable<T> items) =>
            string.Join("", items);

        public static string NewlineSeparated<T>(this IEnumerable<T> items) =>
            items.Separated("\n");
    }
}
