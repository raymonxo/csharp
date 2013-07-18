using System.Collections.Generic;
using System.Linq;

namespace RayMitchell.ProjectEuler.Problems
{
    /// <summary>
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
        private static class CollatzSequences
        {
            // Map from n to collatz sequence length
            private static readonly IDictionary<long, long> Lengths =
                new Dictionary<long, long> { { 1, 1 } };

            public static long Length(long n)
            {
                if (!Lengths.ContainsKey(n))
                    Lengths.Add(n, 1 + Length(n % 2 == 0 ? n / 2 : 3 * n + 1));
                return Lengths[n];
            }
        }

        public static long Solve()
        {
            return (from n in Enumerable.Range(1, 999999)
                    orderby CollatzSequences.Length(n) descending
                    select n).First();
        }
    }
}