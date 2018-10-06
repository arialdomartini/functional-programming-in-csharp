using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using FluentAssertions;
using NSubstitute.Core;
using Xunit;

namespace FunctionalProgramming.Chapter3
{
    public class OptionTest
    {
        [Fact]
        public void null_is_not_a_string()
        {
            string s = null;

            (s is string).Should().BeFalse();
        }
        
        [Fact]
        public void NameValueCollection_returns_null()
        {
            var empty = new NameValueCollection();
            
            var green = empty["green"];
            
            green.Should().BeNull();
        }
        
        [Fact]
        public void Dictionary_throws_expection()
        {
            var empty = new Dictionary<string, string>();

            var action = empty.Invoking(e =>
            {
                var green = e["green"];
            });

            action.Should().Throw<KeyNotFoundException>();
        }
    }

}