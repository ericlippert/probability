using System;
namespace Probability
{
    using SDU = StandardDiscreteUniform;
    static class Episode20
    {
#if false
        probabilistic static IDiscreteDistribution<bool> Flip()
        {
            int x = sample Bernoulli.Distribution(1, 1);
            return x == 0;
        }
#endif 
        // The above should have the same semantics as:

        static IDiscreteDistribution<bool> Flip() => 
            Bernoulli.Distribution(1, 1).Select(x => x == 0);

#if false

        probabilistic static IDiscreteDistribution<int> TwoDSix()
        {
            var d = SDU.Distribution(1, 6);
            int x = sample d;
            int y = sample d;
            return x + y;
        }
#endif
        // The above should have the same semantics as:

        static IDiscreteDistribution<int> TwoDSix() 
        {
            var d = SDU.Distribution(1, 6);
            return d.SelectMany(x => d, (x, y) => x + y);
        }

#if false

        probabilistic static IDiscreteDistribution<string> DoIt(int z)
        {
            int ii = sample TwoDSix();
            if (ii == 2) return "two";
            condition ii != z;
            bool b = sample Flip();
            return b ? "heads" : ii.ToString();
        }
#endif
        // We'll see what this has the same semantics as in Episode 21.

    }
}
