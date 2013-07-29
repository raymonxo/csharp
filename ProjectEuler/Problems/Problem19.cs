using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayMitchell.ProjectEuler.Problems
{
    /// <summary>
    /// You are given the following information, but you may prefer to do
    /// some research for yourself.
    ///
    /// 1 Jan 1900 was a Monday.
    /// Thirty days has September,
    /// April, June and November.
    /// All the rest have thirty-one,
    /// Saving February alone,
    /// Which has twenty-eight, rain or shine.
    /// And on leap years, twenty-nine.
    /// A leap year occurs on any year evenly divisible by 4, but not on a
    /// century unless it is divisible by 400.
    ///
    /// How many Sundays fell on the first of the month during the twentieth
    /// century (1 Jan 1901 to 31 Dec 2000)?
    /// 
    /// Answer: 171
    /// </summary>
    public static class Problem19
    {
        public static int Solve()
        {
            return (from d in new DateTime(1901, 1, 1).DaysUpTo(
                              new DateTime(2000, 12, 31))
                    where d.Day == 1 && d.DayOfWeek == DayOfWeek.Sunday
                    select d).Count();
        }

        public static IEnumerable<DateTime> DaysUpTo(this DateTime from,
                                                     DateTime to)
        {
            for (var i = from; i <= to; i = i.AddDays(1))
                yield return i;
        }
    }
}
