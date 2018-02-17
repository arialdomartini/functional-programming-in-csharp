using System;
using System.IO;
using System.Threading;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming
{
    public class TestConsole : IDisposable
    {
        private readonly TextWriter _originalOut;

        public TestConsole()
        {
            TextWriter = new StringWriter();
            _originalOut = Console.Out;
            Console.SetOut(TextWriter);
        }

        public readonly TextWriter TextWriter;
        
        public static implicit operator string(TestConsole testConsole)
        {
            return testConsole.TextWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(_originalOut);
        }
    }


    public class Client
    {
        [Fact]
        public void should_calculate_execution_time()
        {
            using (var t = new TestConsole())
            {
                PerformanceMonitor.Time("some op", () =>
                {
                    Thread.Sleep(300);
                    return 100;
                });

                string result = t;
                result.Should().Contain("some op took 300ms");
            }
        }
    }
}