using System;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.HighOrderFunctions
{
    public static class FuncExtensions
    {
        public static Func<T2, T1, TR> SwapParameters<T1, T2, TR>(this Func<T1, T2, TR> f)
            => (t1, t2) => f(t2, t1);
    }
    public class ChangeParametersOrder
    {
        [Fact]
        public void should_change_order_of_parameters()
        {
            Func<float, float, float> divide = (x, y) => x / y;
            divide(10, 2).Should().Be(5);

            var divideWithSwappedParameters = divide.SwapParameters();

            divideWithSwappedParameters(10, 2).Should().Be(0.2F);
        }
    }
}