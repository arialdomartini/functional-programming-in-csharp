using System;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.Chapter3
{
    using static F;

    public static partial class F
    {
        public static Some<T> Some<T>(T value) => new Some<T>(value);
        public static None None() => new None();
    }

    public struct None
    {
    }

    public struct Some<T>
    {
        public Some(T value)
        {
            Value = value;
        }

        internal T Value { get; }
    }


    public struct Option<T>
    {
        private Option(T value)
        {
            Value = value;
        }
    
        // Used in
        //     Option<string> none = new None();
        public static implicit operator Option<T>(None _) => 
            new Option<T>();

        // Used in
        //     Option<string> some = new Some("something")
        public static implicit operator Option<T>(Some<T> some) => 
            new Option<T>(some.Value);
        
        // Used in
        //     Option<string> some = "something"
        public static implicit operator Option<T>(T value)
        {
            if (value == null)
                return None();
            return Some(value);
        }

        private T Value { get; }
        
        public TR Match<TR>(Func<TR> left, Func<T, TR> right) => 
            Value != null ? right(Value) : left();
    }
    
    
    public class OptionTest
    {
        [Fact]
        public void match_Nothing_case()
        {
            Option<string> nothing = new None();

            var result = nothing.Match(
                () => true, 
                r => false
            );

            result.Should().Be(true);
        }
        
        [Fact]
        public void match_Some_case()
        {
            Option<string> something = "something";

            var result = something.Match(
                () => -1,
                r => 42
            );

            result.Should().Be(42);
        }
    }
}