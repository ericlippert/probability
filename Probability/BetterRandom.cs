using System;
using System.Threading;
using CRNG = System.Security.Cryptography.RandomNumberGenerator;

namespace Probability
{
    // A crypto-strength, threadsafe, all-static RNG.
    // Still not a great API. We can do better.
    public static class BetterRandom
    {
        private static readonly ThreadLocal<CRNG> crng =
          new ThreadLocal<CRNG>(CRNG.Create);
        private static readonly ThreadLocal<byte[]> bytes =
          new ThreadLocal<byte[]>(() => new byte[sizeof(int)]);
        public static int NextInt()
        {
            crng.Value.GetBytes(bytes.Value);
            return BitConverter.ToInt32(bytes.Value, 0) & int.MaxValue;
        }
        public static double NextDouble()
        {
            while (true)
            {
                long x = NextInt() & 0x001FFFFF;
                x <<= 31;
                x |= (long)NextInt();
                double n = x;
                const double d = 1L << 52;
                double q = n / d;
                if (q != 1.0)
                    return q;
            }
        }
    }
}