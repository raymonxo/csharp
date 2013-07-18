using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RayMitchell.ProjectEuler
{
    public static class MathExtensions
    {
        public static BigInteger Sum(this IEnumerable<BigInteger> source)
        {
            if (source == null)
                throw new ArgumentNullException(("source"));

            return source.Aggregate<BigInteger, BigInteger>(0, (a, x) => a + x);
        }
    }
}
