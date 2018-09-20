using System;
using System.IO;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.Chapter2
{
    public class ImpureCodeBMITest : IDisposable
    {
        private readonly TextWriter _originalOut;
        private readonly TextWriter _out;
        private readonly TextReader _originalIn;

        public ImpureCodeBMITest()
        {
            _originalOut = Console.Out;
            _originalIn = Console.In;
            _out = new StringWriter();
            Console.SetOut(_out);
        }
        
        [Fact]
        public void should_ask_values()
        {
            using (var @in = new StringReader("100\r\n200"))
            {
                Console.SetIn(@in);
                var (weight, height) = BMI.Ask();

                weight.Should().Be(100);
                height.Should().Be(200);

            }
        }
        
        [Fact]
        public void should_ask_and_calculate()
        {
            using (var @in = new StringReader("100\r\n200"))
            {
                Console.SetIn(@in);
                var result = BMI.AskAndEvaluate();

                result.Should().Be("non healthy");
            }
        }

        public void Dispose()
        {
            Console.SetOut(_originalOut);
            Console.SetIn(_originalIn);
            _out.Dispose();
        }
    }
}