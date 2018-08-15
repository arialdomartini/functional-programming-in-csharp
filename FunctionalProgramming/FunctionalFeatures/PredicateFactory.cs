using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.HighOrderFunctions
{
    public class PredicateFactory
    {
        [Fact]
        public void should_generate_predicates_for_Where()
        {
            var range = Enumerable.Range(0, 100);
            
            {
                var onlyEven = range.Where(n => n % 2 == 0);
                onlyEven.Should()
                    .Contain(2);
            }
            
            Func<int, bool> isDivisibleBy(int divisor) =>
                i => i % divisor == 0;
            
            var isDivisibleBy20 = isDivisibleBy(20);

            range.Where(isDivisibleBy20).Count().Should().Be(5);
        }
    }
}