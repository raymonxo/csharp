using System.Collections.Generic;

namespace RayMitchell.ProjectEuler.Problems
{
    /// <summary>
    /// Starting in the top left corner of a 2×2 grid, and only being able
    /// to move to the right and down, there are exactly 6 routes to the
    /// bottom right corner.
    /// 
    /// How many such routes are there through a 20×20 grid?
    /// 
    /// Answer: 137846528820
    /// </summary>
    public class Problem15
    {
        public static long Solve() { return new Grid(20, 20).Routes; }
    }

    struct Grid
    {
        public long X { get; set; }
        public long Y { get; set; }

        public Grid(long x, long y) : this() { X = x; Y = y; }

        public long Routes { get { return GetRoutes(this); } }

        private static long GetRoutes(Grid g)
        {
            // If cache doesn't contain routes for grid
            if (!RoutesCache.ContainsKey(g))
            {
                // Compute routes for grid (1 route if any dimension is 0)
                long routes = g.X == 0 || g.Y == 0
                        ? 1
                        : GetRoutes(new Grid(g.X, g.Y - 1))
                          + GetRoutes(new Grid(g.X - 1, g.Y));

                // Cache routes for grid and its mirror
                RoutesCache.Add(g, routes);
                if (g.X != g.Y)
                    RoutesCache.Add(new Grid(g.Y, g.X), routes);
            }

            return RoutesCache[g];
        }

         private static readonly IDictionary<Grid, long> RoutesCache
            = new Dictionary<Grid, long>();
    }
}
