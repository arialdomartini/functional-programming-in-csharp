using System;
using FluentAssertions;
using Xunit;
using static Xunit.Assert;

namespace FunctionalProgramming.Chapter3
{
    using static F;

    public class Option<T>
    {
        public static Option<T> Nothing() =>
            new Option<T> {IsLeft = true};


        private bool IsLeft { get; set; }

        public void Match(Action left, Action<T> right)
        {
            if (IsLeft) left();
            else right(Value);
        }
    
        public static Option<T> Some(T value) => 
            new Option<T> {Value = value};

        private T Value { get; set; }
    }
    
    public static partial class F
    {
        public static Option<T> Nothing<T>() => 
            Option<T>.Nothing();

        public static Option<T> Some<T>(T value) => 
            Option<T>.Some(value);
    }
    
    public class OptionTest
    {
        [Fact]
        public void match_Nothing_case()
        {
            var nothing = Nothing<string>();

            nothing.Match(
                () => { True(true); }, 
                r => { True(false);});
        }
        
        [Fact]
        public void match_Some_case()
        {
            var something = Some("something");

            something.Match(
                () => { True(false); },
                r => { r.Should().Be("something");});
        }
    }
}