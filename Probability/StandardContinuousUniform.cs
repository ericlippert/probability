namespace Probability
{
    public sealed class StandardContinuousUniform :
        IWeightedDistribution<double>
    {
        public static readonly StandardContinuousUniform
            Distribution = new StandardContinuousUniform();
        private StandardContinuousUniform() { }
        public double Sample() => Pseudorandom.NextDouble();
        public double Weight(double x) => 0.0 <= x & x < 1.0 ? 1.0 : 0.0;
    }
}
