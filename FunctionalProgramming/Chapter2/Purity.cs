using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.Chapter2
{
    public static class StringExtensions
    {
        private static string Capitalize(this string s)
            => s.First().ToString().ToUpper() + s.Substring(1);


        internal static IEnumerable<string> Format(this IEnumerable<string> list)
            => list.Select((s, index) => s.ToLower().Capitalize().Enumerate(index));

        private static string Enumerate(this string s, int index)
        {
            return $"{(index + 1)}. {s}";
        }
    }
    
    public class Purity
    {

        [Fact]
        public void should_format_and_list()
        {
            var shoppingList = new List<string> { "coffee beans", "BANANAS", "Dates" };

            var result = shoppingList.Format();

            result.Should().BeEquivalentTo(new List<string>{
                "1. Coffee beans",
                "2. Bananas",
                "3. Dates"
             });
        }
    }
}