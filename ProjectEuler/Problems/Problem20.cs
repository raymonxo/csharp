using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace RayMitchell.ProjectEuler.Problems
{
    /// <summary>
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
            // Factorial
            Func<int, BigInteger> factorial = null;
            factorial = x => x <= 1 ? 1 : x * factorial(x - 1);

            // Solution
            return (int)(from c in factorial(100).ToString()
                         select Char.GetNumericValue(c)).Sum();
        }
    }
}
