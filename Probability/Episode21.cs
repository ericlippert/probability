using System;
using System.Collections.Generic;
namespace Probability
{
    using System.Collections.Generic;
    using SDU = StandardDiscreteUniform;
    static class Episode21
    {
        public static void DoIt()
        {
            Console.WriteLine("Episode 21");
            AttemptOne.DoIt();
            AttemptTwo.DoIt();
        }
        static class AttemptOne
        {
            public static void DoIt()
            {
                Console.WriteLine("Attempt One: Be like iterator blocks");
                Console.WriteLine(Workflow(3).Histogram());
            }
            // Attempt #1 at lowering: be like "yield return" workflows.
            // * We generate a new class.
            // * Parameters become fields
            // * The body of the method becomes the Sample() method of the class.
            // * sample operators become Sample() calls
            // * failed conditions become restarts.
            // * Not sure yet how to implement Support and Weight

            static IDiscreteDistribution<bool> Flip() =>
                new FlipDistribution();

            sealed class FlipDistribution : IDiscreteDistribution<bool>
            {
                public bool Sample()
                {
                    int x = Bernoulli.Distribution(1, 1).Sample();
                    return x == 0;
                }

                public IEnumerable<bool> Support() =>
                    throw new NotImplementedException();

                public int Weight(bool t) =>
                    throw new NotImplementedException();
            }

            static IDiscreteDistribution<int> TwoDSix() =>
                new TwoDSixDistribution();

            sealed class TwoDSixDistribution : IDiscreteDistribution<int>
            {
                public int Sample()
                {
                    var d = SDU.Distribution(1, 6);
                    int x = d.Sample();
                    int y = d.Sample();
                    return x + y;
                }

                public IEnumerable<int> Support() =>
                    throw new NotImplementedException();

                public int Weight(int t) =>
                    throw new NotImplementedException();
            }

            static IDiscreteDistribution<string> Workflow(int z) =>
                new WorkflowDistribution(z);

            sealed class WorkflowDistribution : IDiscreteDistribution<string>
            {
                int z;

                public WorkflowDistribution(int z)
                {
                    this.z = z;
                }

                public string Sample()
                {
                start:
                    int ii = TwoDSix().Sample();
                    if (ii == 2)
                        return "two";
                    if (!(ii != z))
                        goto start;
                    bool b = Flip().Sample();
                    return b ? "heads" : ii.ToString();
                }

                public IEnumerable<string> Support() =>
                    throw new NotImplementedException();

                public int Weight(string t) =>
                    throw new NotImplementedException();
            }
        }

        static class AttemptTwo
        {

            public static void DoIt()
            {
                Console.WriteLine("Attempt Two: Every statement is a local method");
                Console.WriteLine(WorkflowInlined(3).ShowWeights());
            }

            static IDiscreteDistribution<bool> Flip()
            {
                IDiscreteDistribution<bool> S0(int x) =>
                    Bernoulli.Distribution(1, 1).SelectMany(_x => S1(_x));
                IDiscreteDistribution<bool> S1(int x) => 
                    Singleton<bool>.Distribution(x == 0);
                return S0(0);
            }
            static IDiscreteDistribution<int> TwoDSix()
            {
                IDiscreteDistribution<int> S0(IDiscreteDistribution<int> d, int x, int y) =>
                    S1(StandardDiscreteUniform.Distribution(1, 6), x, y);
                IDiscreteDistribution<int> S1(IDiscreteDistribution<int> d, int x, int y) =>
                    d.SelectMany(_x => S2(d, _x, y));
                IDiscreteDistribution<int> S2(IDiscreteDistribution<int> d, int x, int y) =>
                    d.SelectMany(_y => S3(d, x, _y));
                IDiscreteDistribution<int> S3(IDiscreteDistribution<int> d, int x, int y) =>
                    Singleton<int>.Distribution(x + y);
                return S0(null, 0, 0);
            }

            static IDiscreteDistribution<string> Workflow(int z)
            {
                IDiscreteDistribution<string> S0(int ii, bool b) =>
                    TwoDSix().SelectMany(_ii => S1(_ii, b));
                IDiscreteDistribution<string> S1(int ii, bool b) =>
                    ii == 2 ? S2(ii, b) : S3(ii, b);
                IDiscreteDistribution<string> S2(int ii, bool b) =>
                    Singleton<string>.Distribution("two");
                IDiscreteDistribution<string> S3(int ii, bool b) =>
                    ii != z ? S4(ii, b) : Empty<string>.Distribution;
                IDiscreteDistribution<string> S4(int ii, bool b) =>
                    Flip().SelectMany(_b => S5(ii, _b));
                IDiscreteDistribution<string> S5(int ii, bool b) =>
                    Singleton<string>.Distribution(b ? "heads" : ii.ToString());
                return S0(0, false);
            }

            static IDiscreteDistribution<string> WorkflowInlined(int z) =>
                TwoDSix().SelectMany(ii => 
                    ii == 2 ?
                        Singleton<string>.Distribution("two") :
                        ii != z ?
                            Flip().SelectMany(b => 
                                Singleton<string>.Distribution(b ? 
                                    "heads" : 
                                    ii.ToString())) : 
                            Empty<string>.Distribution);
        }
    }
}
