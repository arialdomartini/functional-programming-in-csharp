using System;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.Chapter3
{
    public class AgeTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(119)]
        [InlineData(120)]
        public void should_allow_valid_ages(int value)
        {
            Age age = value;

            age.Value.Should().Be(value);
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(121)]
        [InlineData(1100)]
        [InlineData(-20)]
        public void should_not_allow_invalid_ages(int value)
        {
            Age age = null;
            
            var action = true.Invoking(_ => age = value);

            action.Should().Throw<Exception>();
        }
    }
}