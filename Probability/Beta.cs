using System;
namespace Probability
{
    using static System.Math;

    public sealed class Beta : IWeightedDistribution<double>
    {
        private readonly double α; 
        private readonly double β;

        public static Beta Distribution(double alpha, double beta)
        {
            if (alpha <= 0.0) throw new ArgumentOutOfRangeException();
            if (beta <= 0.0) throw new ArgumentOutOfRangeException();
            return new Beta(alpha, beta);
        }

        private Beta(double alpha, double beta)
        {
            this.α = alpha;
            this.β = beta;
        }

        public double Sample()
        {
            double x = Gamma.Distribution(α, 1).Sample();
            double y = Gamma.Distribution(β, 1).Sample();
            return x / (x + y);
        }

        // Not normalized
        public double Weight(double x) => x <= 0.0 || x >= 1.0 ? 0.0 : Pow(x, α - 1) * Pow(1 - x, β - 1);

        public override string ToString() =>
            $"Beta({α}, {β})";
    }
}
