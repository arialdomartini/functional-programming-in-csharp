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
        internal static Risk Calculate(Age age, Gender gender) => 
            age > (gender == Gender.Male? 60 : 62 ) ? Risk.High : Risk.Low;
    }

    public class RiskTest
    {
        [Theory]
        [InlineData(20, Gender.Male, Risk.Low)]
        [InlineData(60, Gender.Male, Risk.Low)]
        [InlineData(61, Gender.Male, Risk.High)]

        [InlineData(20, Gender.Female, Risk.Low)]
        [InlineData(60, Gender.Female, Risk.Low)]
        [InlineData(61, Gender.Female, Risk.Low)]
        [InlineData(62, Gender.Female, Risk.Low)]
        [InlineData(63, Gender.Female, Risk.High)]
        public void should_(int age, Gender gender, Risk risk)
        {
            var result = RiskProfile.Calculate(age, gender);

            result.Should().Be(risk);
        }
    }
}