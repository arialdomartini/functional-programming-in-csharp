using System;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.Chapter1.FunctionalFeatures
{
    public static class DecoratorExtensions
    {
        public static Func<T, V> DecorateWithSetupTearDown<T, V>(this Func<T, V> f)
        {
            return t =>
            {
                Setup();
                var result = f(t);
                TearDown();
                return result;
            };
        }

        private static void TearDown()
        {
            TearDownInvoked = true;
        }

        public static bool TearDownInvoked { get; private set; }
        public static bool SetupInvoked { get; private set; }

        private static void Setup()
        {
            SetupInvoked = true;
        }
    }

    public class Decorator
    {
        [Fact]
        public void should_decorate_a_function_adding_setup_and_teardown()
        {
            Func<int, int> doubleMe = i => i * 2;
            doubleMe(10).Should().Be(20);

            var withSetup = doubleMe.DecorateWithSetupTearDown();

            withSetup(10).Should().Be(20);
            DecoratorExtensions.SetupInvoked.Should().Be(true);
            DecoratorExtensions.TearDownInvoked.Should().Be(true);
        }
    }
}