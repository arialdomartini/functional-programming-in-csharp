using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FunctionalProgramming.Chapter1.Exercises
{
    public static class NegatePredicateExtension
    {
        public static Func<T, bool> Negate<T>(this Func<T, bool> predicate)
            => i => !(predicate(i));
    }
    
    public class NegatePredicate
    {
        [Fact]
        public void should_negate_a_unary_predicate()
        {
            Func<int, bool> predicate = i => i % 2 == 0;

            var listTrue = new List<int> {2, 4, 6, 8, 10};
            var listFalse = new List<int> {1, 3, 5, 7, 9};

            listTrue.All(i => predicate(i) == true);
            listFalse.All(i => predicate(i) == false);

            var negate = predicate.Negate();
                        
            listTrue.All(i => negate(i) == false);
            listFalse.All(i => negate(i) == true);

        }
    }
}