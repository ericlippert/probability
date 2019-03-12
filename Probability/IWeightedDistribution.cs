namespace Probability
{
    public interface IWeightedDistribution<T> : IDistribution<T>
    {
        double Weight(T t);
    }
}
