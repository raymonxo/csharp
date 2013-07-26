using System.Collections.Generic;
using System.Linq;

namespace RayMitchell.ProjectEuler.Problems
{
    /// <summary>
    /// Project Euler, Problem 14 (http://projecteuler.net/problem=14).
    /// 
    /// The following iterative sequence is defined for the set of positive
    /// integers:
    /// 
    ///     n → n/2 (n is even)
    ///     n → 3n + 1 (n is odd)
    /// 
    /// Using the rule above and starting with 13, we generate the following
    /// sequence:
    ///     13 → 40 → 20 → 10 → 5 → 16 → 8 → 4 → 2 → 1
    /// 
    /// It can be seen that this sequence (starting at 13 and finishing at 1)
    /// contains 10 terms. Although it has not been proved yet (Collatz
    /// Problem), it is thought that all starting numbers finish at 1.
    /// 
    /// Which starting number, under one million, produces the longest chain?
    /// 
    /// NOTE: Once the chain starts the terms are allowed to go above one
    /// million.
    /// 
    /// Answer: 837799
    /// </summary>
    public class Problem14
    {
        public static long Solve()
        {
            return (from c in CollatzSequence.Range(1, 1000000)
                    orderby c.Length descending
                    select c.StartingNumber).First();
        }
    }

    class CollatzSequence
    {
        public CollatzSequence(long start)
        {
            StartingNumber = start;
        }

        public long Length { get { return GetLength(StartingNumber); } }

        private static readonly IDictionary<long, long> Lengths =
            new Dictionary<long, long> { { 1, 1 } };

        public long StartingNumber { get; private set; }

        private static long GetLength(long n)
        {
            if (!Lengths.ContainsKey(n))
                Lengths.Add(n, 1 + GetLength(n % 2 == 0 ? n / 2 : 3 * n + 1));
            return Lengths[n];
        }

        public static IEnumerable<CollatzSequence> Range(long start, long count)
        {
            while (count-- > 0)
                yield return new CollatzSequence(start++);
        }
    }
}