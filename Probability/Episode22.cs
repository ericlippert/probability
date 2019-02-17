using System;
namespace Probability
{
    static class Episode22
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 22");
            Console.WriteLine(Game(5).ShowWeights());
            Console.WriteLine(Game(5).ExpectedValue()); // 4.03125
        }

        static IDiscreteDistribution<bool> Flip() =>
            Bernoulli.Distribution(1, 1).Select(x => x == 0);
#if false

        probabilistic IDiscreteDistribution<int> Game(int rounds) =>
          (rounds <= 0 || sample Flip()) ? 
            rounds : 
            sample Game(rounds - 1);

        // This lowers to:

        probabilistic IDiscreteDistribution<int> Game(int rounds)
        {
          S0: if (rounds <= 0) goto S5;  
          S1: x = sample Flip();
          S2: if (x) goto S5;  
          S3: y = sample Game(rounds - 1)
          S4: return y;
          S5: return rounds;
        }

        // Which lowers to:
#endif
        static IDiscreteDistribution<int> Game(int rounds)
        {
            IDiscreteDistribution<int> S0() =>
                rounds <= 0 ? S5() : S1();
            IDiscreteDistribution<int> S1() =>
                Flip().SelectMany(x => S2(x));
            IDiscreteDistribution<int> S2(bool x) =>
                x ? S5() : S3();
            IDiscreteDistribution<int> S3() =>
                Game(rounds - 1).SelectMany(y => S4(y));
            IDiscreteDistribution<int> S4(int y) =>
                Singleton<int>.Distribution(y);
            IDiscreteDistribution<int> S5() =>
                Singleton<int>.Distribution(rounds);
            return S0();
        }
    }
}
