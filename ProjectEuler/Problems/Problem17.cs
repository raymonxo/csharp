using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayMitchell.ProjectEuler.Problems
{
    /// <summary>
    /// Project Euler, Problem 17 (http://projecteuler.net/problem=17).
    ///
    /// If the numbers 1 to 5 are written out in words: one, two, three, four,
    /// five, then there are 3 + 3 + 5 + 4 + 4 = 19 letters used in total.
    ///
    /// If all the numbers from 1 to 1000 (one thousand) inclusive were written
    /// out in words, how many letters would be used?
    ///
    /// NOTE: Do not count spaces or hyphens. For example, 342 (three hundred
    /// and forty-two) contains 23 letters and 115 (one hundred and fifteen)
    /// contains 20 letters. The use of "and" when writing out numbers is in
    /// compliance with British usage.
    ///
    /// Answer: 21124
    /// </summary>
    public class Problem17
    {
        public static int Solve()
        {
            return (from n in Enumerable.Range(1, 1000)
                    select n.EnglishName())
                   .SelectMany(c => c.Where(x => x != ' '
                                              && x != '-')).Count();
        }
    }

    static class NumberNames
    {
        // Returns English name of the integer
        public static string EnglishName(this int n)
        {
            var b = new StringBuilder();

            // Places > 10's
            for (var place = n.MostSignificantPlace(); place > 10; place /= 10)
            {
                if (!n.HasDigit(place)) continue;
                if (n >= place * 10)
                    b.Append(" ");
                b.Append(Names[n.Digit(place)] + " " + Names[place]);
            }

            // 10's and 1's places
            if ((n.HasDigit(10) || n.HasDigit(1)) && n >= 100)
                b.Append(" and ");
            if (n.HasDigit(10))
            {
                if (n.Digit(10) == 1)
                    b.Append(Names[n.Digit(10) * 10 + n.Digit(1)]);
                else
                {
                    b.Append(Names[n.Digit(10) * 10]);
                    if (n.HasDigit(1))
                        b.Append("-" + Names[n.Digit(1)]);
                }
            }
            else if (n.HasDigit(1))
                b.Append(Names[n.Digit(1)]);

            return b.ToString();
        }

        // Returns value of digit at given place
        private static int Digit(this int n, int place)
        {
            return n % (place * 10) / place;
        }

        // Returns whether a non-zero digit exists at place
        private static bool HasDigit(this int n, int place)
        {
            return n.Digit(place) >= 1;
        }

        // Returns the most significant place (e.g. 9832 => 1000)
        private static int MostSignificantPlace(this int n)
        {
            var msp = 1;
            while (n > 0)
            {
                msp *= 10;
                n /= 10;
            }
            return msp;
        }

        private static readonly IDictionary<int, string> Names =
            new Dictionary<int, string>
                {
                    { 0, "zero" },
                    { 1, "one" },
                    { 2, "two" },
                    { 3, "three" },
                    { 4, "four" },
                    { 5, "five" },
                    { 6, "six" },
                    { 7, "seven" },
                    { 8, "eight" },
                    { 9, "nine" },
                    { 10, "ten" },
                    { 11, "eleven" },
                    { 12, "twelve" },
                    { 13, "thirteen" },
                    { 14, "fourteen" },
                    { 15, "fifteen" },
                    { 16, "sixteen" },
                    { 17, "seventeen" },
                    { 18, "eighteen" },
                    { 19, "nineteen" },
                    { 20, "twenty" },
                    { 30, "thirty" },
                    { 40, "forty" },
                    { 50, "fifty" },
                    { 60, "sixty" },
                    { 70, "seventy" },
                    { 80, "eighty" },
                    { 90, "ninety" },
                    { 100, "hundred" },
                    { 1000, "thousand" },
                    // Add more entries to handle larger numbers
                };
    }
}
