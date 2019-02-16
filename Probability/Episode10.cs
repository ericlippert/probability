using System;
using System.Linq;
namespace Probability
{
    using SDU = StandardDiscreteUniform;
    static class Episode10
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 10");
            Console.WriteLine("No threes, weighted integer");
            Console.WriteLine(WeightedInteger.Distribution(0, 1, 1, 0, 1, 1, 1)
                .Samples()
                .Take(20)
                .CommaSeparated());

            Console.WriteLine("No threes, conditioned uniform, rejection sampling");
            var noThrees = from roll in SDU.Distribution(1, 6)
                           where roll != 3
                           select roll;
            Console.WriteLine(noThrees.Histogram());

            Console.WriteLine("Ordinary sequence, filter clause closed over variable");
            int filterOut = 3;
            Func<int, bool> predicate = x => x != filterOut;
            var range = Enumerable.Range(1, 6).Where(predicate);
            Console.WriteLine(range.CommaSeparated());
            Console.WriteLine("Change the variable");
            filterOut = 4;
            Console.WriteLine(range.CommaSeparated());

            Console.WriteLine("Distribution, filter clause closed over variable");
            filterOut = 3;
            var d = SDU.Distribution(1, 6).Where(predicate);
            Console.WriteLine(d.Samples().Take(10).CommaSeparated());
            Console.WriteLine("Change the variable");
            filterOut = 4;
            Console.WriteLine(d.Samples().Take(10).CommaSeparated());
            Console.WriteLine("The support is now wrong!");
            Console.WriteLine(d.Support().CommaSeparated());
            Console.WriteLine("We will require that predicates and projections be pure functions.");
        }
    }
}
