using System;
using System.Collections.Generic;

// Let's show how we can create Where out of SelectMany on sequences.
namespace Weird
{
    static class MyLinq
    {
        // Standard implementation of Select:
        public static IEnumerable<R> Select<A, R>(
            this IEnumerable<A> items,
            Func<A, R> projection)
        {
            foreach (A item in items)
                yield return projection(item);
        }

        // Standard implementation of SelectMany:
        public static IEnumerable<R> SelectMany<A, B, R>(
            this IEnumerable<A> items,
            Func<A, IEnumerable<B>> selection,
            Func<A, B, R> projection)
        {
            foreach (A a in items)
                foreach (B b in selection(a))
                    yield return projection(a, b);
        }

        public static IEnumerable<T> Single<T>(T t)
        {
            yield return t;
        }

        public static IEnumerable<T> Zero<T>()
        {
            yield break;
        }

        // Non-standard Where:
        public static IEnumerable<T> Where<T>(
                this IEnumerable<T> items,
                Func<T, bool> predicate) =>
            from a in items
            from b in predicate(a) ? Single(a) : Zero<T>()
            select b;
    }
}
namespace Probability
{
    // No using System.Linq.
    using Weird;
    static class Episode17
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 17");
            Console.WriteLine("Custom Where using only SelectMany");
            Console.WriteLine("aBcDe".Where(char.IsLower).CommaSeparated());
            Console.WriteLine("Delayed throw in enumerator block");
            var foo = Foo(null);
            Console.WriteLine("No throw yet!");
            try
            {
                foreach (int x in foo)
                    Console.WriteLine(x);
            }
            catch
            {
                Console.WriteLine("Now we throw.");
            }
        }

        static IEnumerable<int> Foo(string bar)
        {
            if (bar == null)
                throw new ArgumentNullException();
            yield return bar.Length;
        }
    }
}
