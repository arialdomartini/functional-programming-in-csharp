using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.Chapter1.Exercises
{
    public static class GenericGenericQuickSortExtensions
    {
        private static IEnumerable<T> SmallerThan<T>(this IEnumerable<T> list, T pivot, Func<T, T, int> comparer)
            => list.Where(i => comparer(i, pivot) == -1);

        private static IEnumerable<T> BiggerThan<T>(this IEnumerable<T> list, T pivot, Func<T, T, int> comparer)
            => list.Where(i => comparer(i, pivot) == 1);

        public static IEnumerable<T> GenericQuickSorted<T>(this IEnumerable<T> list, Func<T, T, int> comparer) 
            => !list.Any()
                ? list
                : list.GenericQuickSortedNotEmpty(list.First(), comparer);

        private static IEnumerable<T> GenericQuickSortedNotEmpty<T>(this IEnumerable<T> list, T pivot, Func<T, T, int> comparer)
            => list.SmallerThan(pivot, comparer).GenericQuickSorted(comparer)
                .Append(pivot)
                .Union(list.BiggerThan(pivot, comparer).GenericQuickSorted(comparer));
    }

    public class GenericQuickSort
    {
        [Fact]
        public void should_sort_a_list_of_numbers()
        {
            var list = new List<int> {1, 3, 5, 4, 2, 9, 6, 7, 0, 8};

            int Comparer(int x, int y) => x > y ? 1 : (x < y ? -1 : 0);
            var result = list.GenericQuickSorted(Comparer);

            result.ToList().Should().BeEquivalentTo(
                new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
                option => option.WithStrictOrdering());
        }
    }
}