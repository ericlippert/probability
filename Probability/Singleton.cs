using System.Collections.Generic;
using System.Linq;
namespace Probability
{
    public sealed class Singleton<T> : IDiscreteDistribution<T>
    {
        private readonly T t;
        public static Singleton<T> Distribution(T t) => new Singleton<T>(t);
        private Singleton(T t) => this.t = t;
        public T Sample() => t;
        public IEnumerable<T> Support() => Enumerable.Repeat(this.t, 1);
        public int Weight(T t) => EqualityComparer<T>.Default.Equals(this.t, t) ? 1 : 0;
        public override string ToString() => $"Singleton[{t}]";
    }
}
