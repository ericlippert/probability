namespace Probability
{
    public sealed class StandardContinuousUniform :
        IDistribution<double>
    {
        public static readonly StandardContinuousUniform
            Distribution = new StandardContinuousUniform();
        private StandardContinuousUniform() { }
        public double Sample() => Pseudorandom.NextDouble();
    }
}
