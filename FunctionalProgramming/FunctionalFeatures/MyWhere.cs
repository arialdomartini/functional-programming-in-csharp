using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.HighOrderFunctions
{
    public class MyWhere
    {
        [Fact]
        public void should_filter_based_on_a_predicate()
        {
            var list = new List<string> {"foo", "bar", "barbaz"};

            var result = Where(list, s => s.StartsWith("b"));

            result.Should().BeEquivalentTo(new List<string> {"bar", "barbaz"});
        }

        private static IEnumerable<T> Where<T>(IEnumerable<T> list, Func<T, bool> predicate)
        {
            foreach (var item in list)
            {
                if (predicate(item))
                    yield return item;
            }
        }
    }
}