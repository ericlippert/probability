using System;
namespace Probability
{
    using SDU = StandardDiscreteUniform;
    static class Episode23
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 23");
            var d10 = SDU.Distribution(1, 10);
            var sum = from a in d10
                      from b in d10
                      from c in d10
                      from d in d10
                      let s = a + b + c + d
                      where s >= 20
                      select s;

            Console.WriteLine(sum.Histogram());
        }
    }
}
