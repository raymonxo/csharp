using System.Linq;

using RayMitchell.ProjectEuler.Helpers;

namespace RayMitchell.ProjectEuler.Problems
{
    /// <summary>
    /// Project Euler, Problem 21 (http://projecteuler.net/problem=21).
    /// 
    /// Let d(n) be defined as the sum of proper divisors of n (numbers
    /// less than n which divide evenly into n).  If d(a) = b and d(b) = a,
    /// where a ≠ b, then a and b are an amicable pair and each of a and b
    /// are called amicable numbers.
    ///
    /// For example, the proper divisors of 220 are 1, 2, 4, 5, 10, 11, 20,
    /// 22, 44, 55 and 110; therefore d(220) = 284. The proper divisors of
    /// 284 are 1, 2, 4, 71 and 142; so d(284) = 220.
    ///
    /// Evaluate the sum of all the amicable numbers under 10000.
    ///
    /// Answer: 31626
    /// </summary>
    public static class Problem21
    {
        public static long Solve()
        {
            var q = from n in Enumerable.Range(1, 9999)
                    select new
                    {
                        N = n,
                        D = n.ProperDivisors().Sum()
                    };
            return (from a in q
                    join b in q on a.D equals b.N
                    where b.D == a.N && a.N != b.N
                    select a.N).Sum();
        }
    }
}
