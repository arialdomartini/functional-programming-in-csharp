using System;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.UnitIsBetterThenVoid
{
    public class ActionExtensionsTest
    {
        [Fact]
        public void should_convert_an_action_to_function_returning_Unit()
        {
            var result = -1;
            Action someAction = () => result = 100;

            var func = someAction.ToFunc();

            func();

            result.Should().Be(100);
        }
    }
}