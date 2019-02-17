using System;
using System.IO;
using System.Linq;

namespace Probability
{
    static class Episode25
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 25 -- Random Shakespeare Company");
            var builder = new MarkovBuilder<string>();

            // You provide the corpus.
            var sentences = File.ReadLines("shakespeare.txt")
              .Words()
              .Sentences();

            foreach (var sentence in sentences)
            {
                builder.AddInitial(sentence[0]);
                for (int i = 0; i < sentence.Count - 1; i += 1)
                    builder.AddTransition(sentence[i], sentence[i + 1]);
            }

            var markov = builder.ToDistribution();

            Console.WriteLine(markov
              .Samples()
              .Take(100)
              .Select(x => x.SpaceSeparated())
              .NewlineSeparated());
        }
    }
}
