using System;
namespace Probability
{
    using SDU = StandardDiscreteUniform;
    static class Bonus01
    {
        static IDiscreteDistribution<int> doors = SDU.Distribution(1, 3);

        static IDiscreteDistribution<int> Monty(int car, int you) =>
            from m in doors
            where m != car
            where m != you
            select m;

        public static void DoIt()
        {
            Console.WriteLine("Bonus Episode 01 -- Monty Hall");
            Console.WriteLine("Winning without switching, workflow 1");
            var noSwitch1 =
                from car in doors
                from you in doors
                from monty in Monty(car, you)
                select car == you ? "Win" : "Lose";
            Console.WriteLine(noSwitch1.ShowWeights());

            Console.WriteLine("Winning without switching, workflow 2");
            var noSwitch2 =
                from car in doors
                from you in doors
                from monty in doors
                where monty != car
                where monty != you
                select car == you ? "Win" : "Lose";
            Console.WriteLine(noSwitch2.ShowWeights());
        }
    }
}
