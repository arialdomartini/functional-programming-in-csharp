using System;
using System.IO.MemoryMappedFiles;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using FluentAssertions;
using Xunit;
using static Xunit.Assert;

namespace FunctionalProgramming.Chapter3
{
    using static F;

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
        private readonly bool _isSomething;

        private Option(T value)
        {
            Value = value;
            _isSomething = true;
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
        public static implicit operator Option<T>(T value) => 
            new Option<T>(value);

        private T Value { get; }
        
        public TR Match<TR>(Func<TR> left, Func<T, TR> right) => 
            _isSomething ? right(Value) : left();
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