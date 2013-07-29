using System;

namespace RayMitchell.ProjectEuler.Problems
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Problem 11: " + Problem11.Solve());
            Console.WriteLine("Problem 12: " + Problem12.Solve());
            Console.WriteLine("Problem 13: " + Problem13.Solve1());
            Console.WriteLine("Problem 14: " + Problem14.Solve());
            Console.WriteLine("Problem 15: " + Problem15.Solve());
            Console.WriteLine("Problem 16: " + Problem16.Solve());
            Console.WriteLine("Problem 17: " + Problem17.Solve());
            Console.WriteLine("Problem 17 (long number example): " + MathExtensions.EnglishName(1234567890));
            Console.WriteLine("Problem 18: " + Problem18.Solve());
        }
    }
}
