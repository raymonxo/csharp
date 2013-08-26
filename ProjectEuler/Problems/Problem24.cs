using System;
using System.Collections.Generic;
using System.Linq;

namespace RayMitchell.ProjectEuler.Problems
{
    /// <summary>
    /// Project Euler, Problem 24 (http://projecteuler.net/problem=24).
    /// 
    /// A permutation is an ordered arrangement of objects. For example, 3124
    /// is one possible permutation of the digits 1, 2, 3 and 4. If all of the
    /// permutations are listed numerically or alphabetically, we call it
    /// lexicographic order. The lexicographic permutations of 0, 1 and 2 are:
    ///
    /// 012   021   102   120   201   210
    ///
    /// What is the millionth lexicographic permutation of the digits 0, 1, 2,
    /// 3, 4, 5, 6, 7, 8 and 9?
    ///
    /// Answer: 2783915460
    /// </summary>
    public static class Problem24
    {
        public static long Solve()
        {
            return Convert.ToInt64(
                new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9}
                .Permutations()
                .Skip(999999)
                .First()
                .Aggregate("", (acc, x) => acc + x));   // int[] -> string
        }

        private static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> items)
        {
            var itemsA = items.ToArray();               // Prevents double-iteration
            return itemsA.Length <= 1
                ? new[] { itemsA }
                : from item in itemsA
                  from permutation in itemsA.Where(x => !x.Equals(item)).Permutations()
                  select new [] {item}.Concat(permutation);
        }
    }
}
