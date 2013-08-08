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
                    select n.EnglishName())
                   .SelectMany(c => c.Where(x => x != ' '
                                              && x != '-')).Count();
        }
    }

    public static class MathExtensions
    {
        /// <summary>
        /// Returns English name of the integer.
        ///
        /// Note, although this problem only asked to support English names
        /// for numbers up to 1000, this solution works in general. For
        /// example, calling EnglishName on 1234567890 will return the string:
        ///
        ///     "one billion two hundred thirty-four million five
        ///       hundred sixty-seven thousand eight hundred and ninety"
        /// </summary>
        public static string EnglishName(this int n)
        {
            var b = new StringBuilder();
            foreach (var p in n.Periods())
                b.Append((b.Length > 0 ? " " : "") + p.EnglishName);
            return b.ToString();
        }

        // Returns sequence of the integer's Periods.
        // e.g. 123456789 will return 123, 456, 789
        private static IEnumerable<Period> Periods(this int n)
        {
            var periods = new List<Period>();
            for (int rank = 1; n > 0; ++rank, n /= 1000)
                periods.Insert(0, new Period(rank, n % 1000));
            return periods;
        }
    }

    /// <summary>
    /// Represents a period (3 digit sequence) within an integer.  For example,
    /// the number 123,456,789 contains 3 periods:
    ///     * 789 is the 1st period; it's name is "unit"
    ///     * 456 is the 2nd period; it's name is "thousand"
    ///     * 123 is the 3rd period; it's name is "million"
    /// </summary>
    class Period
    {
        public Period(int rank, int value)
        {
            if (rank < 1)
                throw new ArgumentOutOfRangeException("rank");
            if (value < 0 || value > 1000)
                throw new ArgumentOutOfRangeException("value");

            Rank = rank;
            Value = value;
        }

        public string EnglishName
        {
            get
            {
                var b = new StringBuilder();

                // 100's place
                if (HasHundreds)
                    b.Append(NumberNames[Hundreds] + " " + NumberNames[100]);

                // 10's and 1's place
                if (HasHundreds && (HasTens || HasOnes))
                {
                    b.Append(" ");
                    if (Rank == 1)
                        b.Append("and ");
                }
                if (HasTens)
                {
                    if (Tens == 1)
                        b.Append(NumberNames[Tens * 10 + Ones]);
                    else
                    {
                        b.Append(NumberNames[Tens * 10]);
                        if (HasOnes)
                            b.Append("-" + NumberNames[Ones]);
                    }
                }
                else if (HasOnes)
                    b.Append(NumberNames[Ones]);

                // Period name
                if ((HasHundreds || HasTens || HasOnes) && Rank != 1)
                    b.Append(" " + RankNames[Rank]);

                return b.ToString();
            }
        }

        public int Rank { get; private set; }

        public int Value { get; private set; }

        // Return the digit at the given place
        private int Ones { get { return Digit(1); } }
        private int Tens { get { return Digit(10); } }
        private int Hundreds { get { return Digit(100); } }

        // Return whether the digit at the given place is non-zero
        private bool HasOnes { get { return Ones > 0; } }
        private bool HasTens { get { return Tens > 0; } }
        private bool HasHundreds { get { return Hundreds > 0; } }

        private static readonly IDictionary<int, string> NumberNames =
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
                };

        private static readonly IDictionary<int, string> RankNames =
            new Dictionary<int, string>
                {
                    { 1, "unit" },
                    { 2, "thousand" },
                    { 3, "million" },
                    { 4, "billion" },
                    { 5, "trillion" },
                    // Add more entries to handle more periods
                };

        private int Digit(int place) { return Value % (place * 10) / place; }
    }
}
