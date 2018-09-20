using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.Chapter2
{
    public class BMITest
    {
        [Fact]
        public void should_calculate_bmi()
        {
            var result = BMI.Calculate(weight: 185M, height: 1.72M);

            result.Should().Be(62.533802055164954029204975663M);
        }
        
        [Theory]
        [InlineData(185, 10, "non healthy")]
        public void should_evaluate_bmi(decimal weight, decimal height, string expected)
        {
            var result = BMI.Evaluate(BMI.Calculate, weight, height);

            result.Should().Be(expected);
        }
    }
}