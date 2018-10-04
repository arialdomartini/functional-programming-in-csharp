using System;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.Chapter3
{
    public class Age
    {
        public readonly int Value;

        private Age(int value)
        {
            if (!IsValid(value))
                throw new ArgumentException($"{value} is not a valid age");
            Value = value;
        }

        private bool IsValid(int value) => 
            value >= 0 && value <= 120;

        public static implicit operator Age(int value)
        {
            return new Age(value);
        }
        
        public static bool operator >(Age l, int r) =>
            l.Value > r;
        
        public static bool operator <(Age l, int r) =>
            l.Value < r;
    }

    public enum Risk
    {
        Low, High
    }

    public static class RiskProfile
    {
        internal static Risk Calculate(Age age) => age > 60 ? Risk.Low : Risk.High;
    }
    
    public class RiskTest
    {
        [Theory]
        [InlineData(20, Risk.Low)]
        [InlineData(60, Risk.Low)]
        [InlineData(61, Risk.High)]
        public void should_(int age, Risk risk)
        {
            var result = RiskProfile.Calculate(age);

            result.Should().Be(risk);
        }
    }

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