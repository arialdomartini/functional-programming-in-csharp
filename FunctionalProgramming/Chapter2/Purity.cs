using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace FunctionalProgramming.Chapter2
{
    public static class StringExtensions
    {
        internal static string Capitalize(this string s)
            => s.First().ToString().ToUpper() + s.ToLower().Substring(1);

        internal static IEnumerable<string> Format(this IEnumerable<string> list)
            => list.AsParallel().Select((s, index) => s.Capitalize().PrependNumeration(index + 1));

        internal static string PrependNumeration(this string s, int index)
        {
            return $"{(index)}. {s}";
        }
    }
    
    public class Purity
    {
        private ITestOutputHelper _outp;

        public Purity(ITestOutputHelper outp)
        {
            _outp = outp;
        }

        [Fact]
        public void should_format_and_list_non_functional_approach()
        {
            var shoppingList = new List<string> { "coffee beans", "BANANAS", "Dates" };

            var result = FormatNonFunctional(shoppingList);

            result.Should().BeEquivalentTo(new List<string>{
                "1. Coffee beans",
                "2. Bananas",
                "3. Dates"
            });
        }

        [Fact]
        public void should_format_and_list_functional_approach()
        {
            var shoppingList = new List<string> { "coffee beans", "BANANAS", "Dates" };

            var result = FormatFunctional(shoppingList);

            result.Should().BeEquivalentTo(new List<string>{
                "1. Coffee beans",
                "2. Bananas",
                "3. Dates"
            });
        }

        private IEnumerable<string> FormatNonFunctional(IEnumerable<string> shoppingList)
        {
            var index = 1;
            var result = new List<string>();
            
            var list = shoppingList.ToList();
            _outp.WriteLine(list.Count.ToString());

            list.ForEach(item =>
            {
                _outp.WriteLine(item);
                result.Add(item.Capitalize().PrependNumeration(index));
                index = index + 1;
            });
            
            return result;
        }
        
        private IEnumerable<string> ConcurrentFormatNonFunctional_not_working(IEnumerable<string> shoppingList)
        {
            var index = 1;
            var result = new List<string>();
            
            var list = shoppingList.ToList();
            _outp.WriteLine(list.Count.ToString());

            Parallel.ForEach(list, item =>
            {
                _outp.WriteLine(item);
                result.Add(item.Capitalize().PrependNumeration(index));
                index = index + 1;
            });
            
            return result;
        }
        private IEnumerable<string> WithSelectFormatNonFunctional2_not_working(IEnumerable<string> shoppingList)
        {
            var index = 1;
            var result = new List<string>();
            
            var list = shoppingList.ToList();
            _outp.WriteLine(list.Count.ToString());

            list.Select(item =>
            {
                _outp.WriteLine(item);
                result.Add(item.Capitalize().PrependNumeration(index));
                index = index + 1;
                return "unused";
            });
            
            return result;
        }

        private IEnumerable<string> FormatFunctional(IReadOnlyCollection<string> shoppingList) => 
            shoppingList.Format();


        [Fact]
        public void functional_approach_should_perform_better_than_non_functional()
        {
            var shoppingList = GenerateRandom(numberOfItems: 1_000);

            var elapsedFunctional = Benchmark(() => FormatFunctional(shoppingList.ToImmutableList()));
            var elapsedNonFunctional = Benchmark(() => FormatNonFunctional(shoppingList));

            elapsedFunctional.Should()
                .BeLessOrEqualTo(elapsedNonFunctional);
        }

        private static double Benchmark(Func<IEnumerable<string>> func)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            func();
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        private static IEnumerable<string> GenerateRandom(int numberOfItems)
        {
            var random = new Random();
            
            var validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";
            string RandomString()
            => string.Concat(
                Enumerable.Range(0, random.Next(1, 30))
                    .Select(i => validChars[random.Next(validChars.Length)]).ToList());

            return Enumerable.Range(0, numberOfItems)
                .Select(i => RandomString());
        }
    }
}