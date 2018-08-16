using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace FunctionalProgramming.Chapter1.Exercises
{
    public static class QuickSortExtensions
    {
        private static IEnumerable<int> SmallerThan(this IEnumerable<int> list, int pivot)
            => list.Where(i => i < pivot);

        private static IEnumerable<int> BiggerThan(this IEnumerable<int> list, int pivot)
            => list.Where(i => i > pivot);

        public static IEnumerable<int> QuickSorted(this IEnumerable<int> list) 
            => !list.Any()
                ? list
                : list.QuickSortedNotEmpty(list.First());

        private static IEnumerable<int> QuickSortedNotEmpty(this IEnumerable<int> list, int pivot)
            => list.SmallerThan(pivot).QuickSorted()
                .Append(pivot)
                .Union(list.BiggerThan(pivot).QuickSorted());
    }

    public class QuickSort
    {
        [Fact]
        public void should_sort_a_list_of_numbers()
        {
            var list = new List<int> {1, 3, 5, 4, 2, 9, 6, 7, 0, 8};

            var result = list.QuickSorted();

            result.ToList().Should().BeEquivalentTo(
                new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
                option => option.WithStrictOrdering());
        }
    }
}