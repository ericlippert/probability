using System;
namespace Probability
{
    using static System.Math;

    public sealed class Gamma : IWeightedDistribution<double>
    {
        private readonly double k; // shape
        private readonly double θ; // scale

        public static Gamma Distribution(double shape, double scale)
        {
            if (shape <= 0.0) throw new ArgumentOutOfRangeException();
            if (scale <= 0.0) throw new ArgumentOutOfRangeException();
            return new Gamma(shape, scale);
        }

        private Gamma(double shape, double scale)
        {
            this.k = shape;
            this.θ = scale;
        }

        public double Sample()
        {
            int n = (int)Floor(k);
            double δ = k - n;
            double ξ = 0.0;
            double η = 0.0;
            double s = 0.0;
            var scu = StandardContinuousUniform.Distribution;

            for (int i = 0; i < n; ++i)
                s += Log(scu.Sample());

            if (δ > 0.0)
            {
                // Ahrens-Dieter 
                do
                {
                    double u = scu.Sample();
                    double v = scu.Sample();
                    double w = scu.Sample();
                    if (u <= E / (E + δ))
                    {
                        ξ = Pow(v, 1 / δ);
                        η = w * Pow(ξ, δ - 1);
                    }
                    else
                    {
                        ξ = 1 - Log(v);
                        η = w * Exp(-ξ);
                    }
                } while (η > Pow(ξ, δ - 1) * Exp(-ξ));
            }
            return θ * (ξ - s);
        }

        // Not normalized
        public double Weight(double x) => x <= 0.0 ? 0.0 : Pow(x, k - 1) * Exp(-x / θ);

        public override string ToString() =>
            $"Gamma({k}, {θ})";
    }
}
