using System;

namespace FunctionalProgramming.Chapter2
{
    public static class BMI
    {

        public static decimal Calculate(decimal weight, decimal height) => 
            weight / (height * height);

        public static Func<decimal, decimal, string> Evaluate(Func<decimal, decimal, decimal> calculate) => 
            (weight, height) =>
                calculate(weight, height) == 18.5M ? "healthy" : "non healthy";

        public static string AskAndEvaluate()
        {
            var (weight, height) = Ask();
            return Evaluate(Calculate)(weight, height);
        }

        public static (decimal, decimal) Ask()
        {
            Console.WriteLine("Your weight?");
            var response = Console.ReadLine();
            var weight = decimal.Parse(response);
            
            Console.WriteLine("Your height?");
            var response2 = Console.ReadLine();
            var height = decimal.Parse(response2);

            return (weight, height);
        }
    }
}