using System;
using System.Collections.Generic;

namespace Probability
{
    public sealed class Metropolis<T> : IWeightedDistribution<T>
    {
        private readonly IEnumerator<T> enumerator;
        private readonly Func<T, double> target;
        public static Metropolis<T> Distribution(
            Func<T, double> target, 
            IDistribution<T> initial, 
            Func<T, IDistribution<T>> proposal)
        {
            var markov = Markov<T>.Distribution(initial, transition);
            return new Metropolis<T>(target, markov.Sample().GetEnumerator());
            IDistribution<T> transition(T d)
            {
                T candidate = proposal(d).Sample();
                return Flip<T>.Distribution(candidate, d, target(candidate) / target(d));
            }
        }

        private Metropolis(Func<T, double> target, IEnumerator<T> enumerator)
        {
            this.enumerator = enumerator;
            this.target = target;
        }

        public T Sample()
        {
            this.enumerator.MoveNext();
            return this.enumerator.Current;
        }

        public double Weight(T t) => this.target(t);
    }
}

