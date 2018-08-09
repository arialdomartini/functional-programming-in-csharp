using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.ImmutabilityAndConcurrency
{
    public class Test
    {
        private readonly MutatingStateFromConcurrentProcesses _sut;

        public Test()
        {
            _sut = new MutatingStateFromConcurrentProcesses();
        }
    
        [Fact]
        public void mutating_state_from_concurrent_processes_gives_unpredictable_results()
        {
            var (result1, result2) = _sut.RunMutatingState();

            result1.Should().NotBe(result2);
            result2.Should().Be(0);
        }

        [Fact]
        public void TestWithoutMutatingState()
        {
            var (result1, result2) = _sut.RunWithoutMutatingState();

            result1.Should().Be(0);
            result2.Should().Be(0);
        }

    }
}