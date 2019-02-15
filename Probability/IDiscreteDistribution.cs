using System.Collections.Generic;
namespace Probability
{
    public interface IDiscreteDistribution<T> : IDistribution<T>
    {
        IEnumerable<T> Support();
        int Weight(T t);
    }
}
