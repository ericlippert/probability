using System.Collections.Generic;
namespace Probability
{
    public interface IDiscreteDistribution<T> : IWeightedDistribution<T>
    {
        IEnumerable<T> Support();
        new int Weight(T t);
    }
}
