using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using static System.Linq.Enumerable;

namespace FunctionalProgramming.Chapter2
{
    public static class StringExtensions
    {
        internal static string Capitalize(this string s)
            => s.ToUpper()[0] + s.ToLower().Substring(1);

        internal static string PrependNumeration(this string s, int index) => 
            $"{index}. {s}";

        internal static string ToSentenceCase(this string s) =>
            s.ToUpper()[0] + s.ToLower().Substring(1);
    }
    
    public class Purity
    {
        private readonly ITestOutputHelper _outp;

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

        private static IEnumerable<string> FormatBonannosFunctional(List<string> list) => 
            list
                .Select(StringExtensions.ToSentenceCase)
                .Zip(Range(1, list.Count), (s, i) => $"{i}. {s}");

        [Fact]
        public void should_format_and_list_with_Bonanno_s_functional_approach()
        {
            var shoppingList = new List<string> { "coffee beans", "BANANAS", "Dates" };

            var result = FormatBonannosFunctional(shoppingList);

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
        
        [Fact]
        public void should_work_with_Any()
        {
            var shoppingList = new List<string> { "coffee beans", "BANANAS", "Dates" };

            var result = bad_implementation_with_select_and_side_effects(shoppingList);

            result.Should().BeEquivalentTo(new List<string>{
                "1. Coffee beans",
                "2. Bananas",
                "3. Dates"
            });
        }

        private IEnumerable<string> FormatNonFunctional(List<string> shoppingList)
        {
            var index = 1;
            var result = new List<string>();

            _outp.WriteLine(shoppingList.Count.ToString());

            shoppingList.ForEach(item =>
            {
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

        private IEnumerable<string> bad_implementation_with_select_and_side_effects(IEnumerable<string> shoppingList)
        {
            var index = 1;
            var result = new List<string>();
            
            var list = shoppingList.ToList();
            _outp.WriteLine(list.Count.ToString());

            // This is a very wrong implementation, as it relies on
            // side effect. "Any" is actually meant to be used with
            // pure functions.
            var enumerable = list.Select(item =>
            {
                _outp.WriteLine(item);
                result.Add(item.Capitalize().PrependNumeration(index));
                index = index + 1;
                return index.ToString();
            });


            // Commenting this statment makes the previous Select to be
            // removed!
            enumerable.ToList().ForEach(item =>
                _outp.WriteLine($"I got {item}"));
            return result;
        }

        private IEnumerable<string> FormatFunctional(List<string> shoppingList) => 
            shoppingList
            .Select(StringExtensions.Capitalize)
            .Zip(Range(1, shoppingList.Count), (i, j) => $"{i}. {j}");

        

        [Fact]
        public void functional_approach_should_perform_better_than_non_functional()
        {
            var list = GenerateRandom(numberOfItems: 6_000).ToList();
         
            var elapsedBonanno = 
                Benchmark(() => FormatBonannosFunctional(list));
            var elapsedFunctional = 
                Benchmark(() => FormatFunctional(list));
            var elapsedNonFunctional =
//                Benchmark(() => FormatNonFunctional(list));
                0;          
            _outp.WriteLine($"functional: {elapsedFunctional}");
            _outp.WriteLine($"Bonanno: {elapsedBonanno}");
            _outp.WriteLine($"non-functional: {elapsedNonFunctional}");
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