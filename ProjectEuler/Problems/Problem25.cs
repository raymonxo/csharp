using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RayMitchell.ProjectEuler.Problems
{
    /// <summary>
    /// Project Euler, Problem 25 (http://projecteuler.net/problem=25).
    /// 
    /// The Fibonacci sequence is defined by the recurrence relation:
    ///
    ///     Fn = Fn−1 + Fn−2, where F1 = 1 and F2 = 1.
    ///
    /// Hence the first 12 terms will be:
    ///
    ///     F1 = 1
    ///     F2 = 1
    ///     F3 = 2
    ///     F4 = 3
    ///     F5 = 5
    ///     F6 = 8
    ///     F7 = 13
    ///     F8 = 21
    ///     F9 = 34
    ///     F10 = 55
    ///     F11 = 89
    ///     F12 = 144
    ///
    /// The 12th term, F12, is the first term to contain three digits.
    ///
    /// What is the first term in the Fibonacci sequence to contain 1000
    /// digits?
    ///
    /// Answer: 4782
    /// </summary>
    public static class Problem25
    {
        public static int Solve()
        {
            return Fibonaccis()
                .Select((n, i) => new {N = n, Term = i + 1})
                .SkipWhile(x => x.N.ToString().Length < 1000)
                .First()
                .Term;
        }

        private static IEnumerable<BigInteger> Fibonaccis()
        {
            var nMinus1 = new BigInteger(1);
            var nMinus2 = new BigInteger(1);

            yield return nMinus1;
            yield return nMinus2;

            while (true)
            {
                var n = nMinus1 + nMinus2;
                nMinus2 = nMinus1;
                nMinus1 = n;
                yield return n;
            }
        }
    }
}
