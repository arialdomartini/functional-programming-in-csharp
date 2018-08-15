using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.FunctionalFeatures
{
    public class DictionarySyntax
    {
        [Fact]
        public void CSharp6_dictionary_initializer_syntax()
        {
            var classic = new Dictionary<string, string>
            {
                { "true", "vrai"},
                { "false", "faux"}
            };
            
            var withAdd = new Dictionary<string, string>();
            withAdd.Add("true", "vrai");
            withAdd.Add("false", "faux");

            // Notice that var cannot be use. Add KeyValuePair is available only
            // with IDictionary<T, T> interface
            IDictionary<string, string> withKeyValuePair = new Dictionary<string, string>();
            withKeyValuePair.Add(new KeyValuePair<string, string>("true", "vrai"));
            withKeyValuePair.Add(new KeyValuePair<string, string>("flalse", "faux"));


            var withCSharp6Syntax = new Dictionary<string, string>
            {
                ["true"] = "vrai",
                ["false"] = "faux"
            };


            var result = withCSharp6Syntax["true"];
            
            result.Should().Be(classic["true"]);
            result.Should().Be(withKeyValuePair["true"]);
            result.Should().Be(withAdd["true"]);
        }
    }
}