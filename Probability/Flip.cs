namespace Probability
{
    using SCU = StandardContinuousUniform;
    sealed class Flip<T> : IWeightedDistribution<T>
    {
        private readonly T heads;
        private readonly T tails;
        private readonly double p;
        public static IWeightedDistribution<T> Distribution(T heads, T tails, double p)
        {
            if (p <= 0.0) 
                return Singleton<T>.Distribution(tails);
            if (p >= 1.0) 
                return Singleton<T>.Distribution(heads);
            if (heads.Equals(tails)) 
                return Singleton<T>.Distribution(heads);
            return new Flip<T>(heads, tails, p);
        }
        private Flip(T heads, T tails, double p)
        {
            this.heads = heads;
            this.tails = tails;
            this.p = p;
        }
        public T Sample() =>
            SCU.Distribution.Sample() <= p ? heads : tails;
        public double Weight(T t) => 
            t.Equals(heads) ? p : t.Equals(tails) ? 1.0 - p : 0.0;
    }
}
