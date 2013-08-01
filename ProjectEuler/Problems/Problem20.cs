using System;
using System.Linq;
using System.Numerics;

namespace RayMitchell.ProjectEuler.Problems
{
    /// <summary>
    /// Project Euler, Problem 20 (http://projecteuler.net/problem=20).
    /// 
    /// n! means n × (n − 1) × ... × 3 × 2 × 1
    ///
    /// For example, 10! = 10 × 9 × ... × 3 × 2 × 1 = 3628800,
    /// and the sum of the digits in the number 10! is
    /// 3 + 6 + 2 + 8 + 8 + 0 + 0 = 27.
    ///
    /// Find the sum of the digits in the number 100!
    ///
    /// Answer: 648
    /// </summary>
    class Problem20
    {
        public static int Solve()
        {
            Func<int, BigInteger> factorial = null;
            factorial = n => n <= 1 ? 1 : n * factorial(n - 1);

            return (int)(from c in factorial(100).ToString()
                         select Char.GetNumericValue(c)).Sum();
        }
    }
}
