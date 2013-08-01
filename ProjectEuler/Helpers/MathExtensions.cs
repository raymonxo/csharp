using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RayMitchell.ProjectEuler.Helpers
{
    public static class MathExtensions
    {
        public static IEnumerable<int> Divisors(this int value)
        {
            for (int i = 1, max = (int)Math.Sqrt(value); i <= max; ++i)
                if (value % i == 0)
                {
                    yield return i;
                    yield return value / i;
                }
        }

        public static IEnumerable<int> ProperDivisors(this int value)
        {
            return value.Divisors().Where(x => x != value).Distinct();
        }

        public static BigInteger Sum(this IEnumerable<BigInteger> source)
        {
            if (source == null)
                throw new ArgumentNullException(("source"));

            return source.Aggregate<BigInteger, BigInteger>(0, (a, x) => a + x);
        }
    }
}
