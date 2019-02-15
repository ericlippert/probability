using System;
using System.Collections.Generic;

abstract class Animal { }
sealed class Cat : Animal { }
sealed class Dog : Animal { }
sealed class Goldfish : Animal { }

namespace Probability
{
    static class Episode06
    {
        public static void DoIt()
        {
            var cat = new Cat();
            var dog = new Dog();
            var fish = new Goldfish();
            var animals = new List<Animal>() { cat, dog, dog, fish };
            Console.WriteLine(animals.ToUniform().Histogram());
            Console.WriteLine(animals.ToUniform().ShowWeights());
        }
    }
}
