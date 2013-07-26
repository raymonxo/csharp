using System;
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
                    select new Number(n).Name)
                   .SelectMany(c => c.Where(x => x != ' '
                                              && x != '-')).Count();
        }
    }

    class Number
    {
        public Number(int value)
        {
            Value = value;
        }

        public string Name
        {
            get
            {
                var b = new StringBuilder();

                // Positions > 10's
                for (var pos = MostSignificantPosition(); pos > 10; pos /= 10)
                {
                    if (!HasDigit(pos)) continue;
                    if (Value >= pos * 10)
                        b.Append(" ");
                    b.Append(Names[Digit(pos)] + " " + Names[pos]);
                }

                // 10's and 1's positions
                if ((HasDigit(10) || HasDigit(1)) && Value >= 100)
                    b.Append(" and ");
                if (HasDigit(10))
                {
                    if (Digit(10) == 1)
                        b.Append(Names[Digit(10) * 10 + Digit(1)]);
                    else
                    {
                        b.Append(Names[Digit(10) * 10]);
                        if (HasDigit(1))
                            b.Append("-" + Names[Digit(1)]);
                    }
                }
                else if (HasDigit(1))
                    b.Append(Names[Digit(1)]);

                return b.ToString();
            }
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

        public int Value { get; private set; }

        // Returns digit value at given position
        private int Digit(int position)
        {
            return Value % (position * 10) / position;
        }

        // Returns whether a non-zero digit exists at position
        private bool HasDigit(int position)
        {
            return Digit(position) >= 1;
        }

        // Returns value of most significant position (e.g. 1000)
        private int MostSignificantPosition()
        {
            var msp = 1;
            for (var v = Value; v > 0; v /= 10)
                msp *= 10;
            return msp;
        }
    }
}
