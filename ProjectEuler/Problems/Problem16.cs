using System;
using System.Linq;
using System.Numerics;

namespace RayMitchell.ProjectEuler.Problems
{
    /// <summary>
    /// Project Euler, Problem 16 (http://projecteuler.net/problem=16).
    ///
    /// 215 = 32768 and the sum of its digits is 3 + 2 + 7 + 6 + 8 = 26.
    ///
    /// What is the sum of the digits of the number 2^1000?
    ///
    /// Answer: 1366
    /// </summary>
    class Problem16
    {
        public static int Solve()
        {
            return (from c in BigInteger.Pow(2, 1000).ToString()
                    select (int)Char.GetNumericValue(c)).Sum();
        }
    }
}