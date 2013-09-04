using System;
using System.IO;
using System.Linq;

namespace RayMitchell.ProjectEuler.Problems
{
    public static class Problem22
    {
        /// <summary>
        /// Project Euler, Problem 22 (http://projecteuler.net/problem=22).
        /// 
        /// Using names.txt (right click and 'Save Link/Target As...'), a 46K
        /// text file containing over five-thousand first names, begin by
        /// sorting it into alphabetical order. Then working out the
        /// alphabetical value for each name, multiply this value by its
        /// alphabetical position in the list to obtain a name score.
        ///
        /// For example, when the list is sorted into alphabetical order,
        /// COLIN, which is worth 3 + 15 + 12 + 9 + 14 = 53, is the 938th name
        /// in the list. So, COLIN would obtain a score of 938 × 53 = 49714.
        ///
        /// What is the total of all the name scores in the file?
        /// 
        /// Answer: 871198282
        /// </summary>
        public static int Solve()
        {
            return File.ReadAllText("names.txt")
                .Split(new [] {'"', ','}, StringSplitOptions.RemoveEmptyEntries)
                .OrderBy(n => n)
                .Select((n, i) => new {Name = n, Position = i + 1})
                .Sum(x => x.Position * x.Name.Sum(c => c - 'A' + 1));
        }
    }
}
