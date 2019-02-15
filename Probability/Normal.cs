namespace Probability
{
    using static System.Math;
    using SCU = StandardContinuousUniform;
    public sealed class Normal : IDistribution<double>
    {
        public double Mean { get; }
        public double Sigma { get; }
        public double μ => Mean;
        public double σ => Sigma;
        public readonly static Normal Standard = Distribution(0, 1);
        public static Normal Distribution(
            double mean, double sigma) =>
          new Normal(mean, sigma);
        private Normal(double mean, double sigma)
        {
            this.Mean = mean;
            this.Sigma = sigma;
        }
        // Box-Muller method
        private double StandardSample() =>
          Sqrt(-2.0 * Log(SCU.Distribution.Sample())) *
            Cos(2.0 * PI * SCU.Distribution.Sample());
        public double Sample() => μ + σ * StandardSample();
    }
}
