using System;
using System.Collections.Generic;
using System.Linq;
namespace Probability
{
    using SCU = StandardContinuousUniform;
    public sealed class Bernoulli : IDiscreteDistribution<int>
    {
        public static IDiscreteDistribution<int> Distribution(int zero, int one)
        {
            if (zero < 0 || one < 0 || zero == 0 && one == 0)
                throw new ArgumentException();
            if (zero == 0)
                return Singleton<int>.Distribution(1);
            if (one == 0)
                return Singleton<int>.Distribution(0);
            return new Bernoulli(zero, one);
        }
        public int Zero { get; }
        public int One { get; }
        private Bernoulli(int zero, int one)
        {
            this.Zero = zero;
            this.One = one;
        }
        public int Sample() => (SCU.Distribution.Sample() <= ((double)Zero) / (Zero + One)) ? 0 : 1;
        public IEnumerable<int> Support() => Enumerable.Range(0, 2);
        public int Weight(int x) => x == 0 ? Zero : x == 1 ? One : 0;
        public override string ToString() => $"Bernoulli[{this.Zero}, {this.One}]";
    }
}

