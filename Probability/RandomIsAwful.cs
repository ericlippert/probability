using System;
using System.Threading;

namespace Probability
{
    static class RandomIsAwful
    {
        static Random shared = new Random();
        static string s = "";
        public static void DoIt()
        {

            Console.WriteLine(
@"Episode 1: Random is awful
In earlier days this would print 100 of the same number.
That bug has been fixed, but you'll note we still get no sixes.");

            for (int i = 0; i < 100; ++i)
            {
                Random random = new Random();
                Console.Write(random.Next(1, 6) + " ");
            }
            Console.WriteLine();

            Console.WriteLine(
@"Similarly, in earlier days this would eventually print all zeros;
Random is not thread safe, and its common failure mode
is to get into a state where it can only produce zero.
This bug has also been fixed, though this is still a bad idea");

            for (int i = 0; i < 100; ++i)
            {
                new Thread(() => s += shared.Next(1, 6) + " ").Start();
            }

            // Yeah I should wait for those to finish. Oh well.
            Console.WriteLine(s);

            Console.WriteLine(
@"The real problem though is that this interface is not strong enough
to do all the interesting stuff we want to do with stochastic programming
in the modern era. That's what we'll be exploring in this series.");
        }
    }
}
