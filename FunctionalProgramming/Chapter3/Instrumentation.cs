using System;
using System.Diagnostics;
using FluentAssertions;
using Xunit;
using Unit = System.ValueTuple;

namespace FunctionalProgramming.Chapter3
{
    using static F;

    public static partial class F
    {
        public static Unit Unit() => default(Unit);

        public static Func<T, Unit> ToFunc<T>(this Action<T> action) =>
            t => 
            {
                action(t);
                return Unit();
            };
    }

    public static class Instrumentation
    {
        public static T Time<T>(string op, Func<T> f)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var result = f();
            
            stopwatch.Stop();
            Console.WriteLine($"{op} took {stopwatch.ElapsedMilliseconds} ms");
            
            return result;
        }
        
        // This is horrible (a lot of duplication)
        public static void Time(string op, Action f)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            f();
            
            stopwatch.Stop();
            Console.WriteLine($"{op} took {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    public class InstrumentationTest
    {
        [Fact]
        public void should_invoke_a_function_returning_bool()
        {
            var invoked = false;
            
            Func<bool> func = () => { 
                invoked = true;
                return true;
            };

            Instrumentation.Time("op", func);

            invoked.Should().Be(true);
        }

        [Fact]
        public void should_invoke_a_function_returning_a_Unit_that_is_an_Action()
        {
            var invoked = false;
            
            Func<Unit> func = () => { 
                invoked = true;
                return Unit();
            };

            Instrumentation.Time("op", func);

            invoked.Should().Be(true);
        }
        
        [Fact]
        public void should_convert_an_Action_to_a_Func_returning_Unit()
        {
            string got = null;
            Action<string> action = s => { got = s; };

            var func = action.ToFunc();
            
            var result = func("some string");
            result.Should().Be(Unit());
            got.Should().Be("some string");
        }
    }
}