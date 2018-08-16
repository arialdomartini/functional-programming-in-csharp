using System;
using System.Threading;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.Chapter1.UnitIsBetterThenVoid
{
    public class PerformanceMonitorTest
    {
        [Fact]
        public void should_calculate_execution_time()
        {
            using (var t = new TestConsole())
            {
                Func<int> func = () =>
                {
                    Thread.Sleep(300);
                    return 100;
                };
                
                PerformanceMonitor.Time("some op", func);

                string result = t;
                result.Should().Contain("some op took 300ms");
            }
        }

        [Fact]
        public void should_work_with_Actions_as_well()
        {
            using (var t = new TestConsole())
            {
                Action action = () =>
                {
                    Thread.Sleep(300);
                };
                
                PerformanceMonitor.Time("some op", action);

                string result = t;
                result.Should().Contain("some op took 300ms");
            }
        }
    }
}