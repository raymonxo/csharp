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
        public static long Solve()
        {
            return new Grid(20, 20).RouteCount;
        }
    }

    struct Grid
    {
        public Grid(long x, long y)
            : this()
        {
            X = x;
            Y = y;
        }

        public long RouteCount { get { return GetRouteCount(this); } }

        // Cache mapping grid to route count
        private static readonly IDictionary<Grid, long> RouteCounts
            = new Dictionary<Grid, long>();
        
        public long X { get; private set; }

        public long Y { get; private set; }

        private static long GetRouteCount(Grid g)
        {
            // If cache doesn't contain entry grid
            if (!RouteCounts.ContainsKey(g))
            {
                // Compute route count for grid
                var routes = g.X == 0 || g.Y == 0
                        ? 1  // Only one route if either dimension is zero
                        : GetRouteCount(new Grid(g.X, g.Y - 1))
                          + GetRouteCount(new Grid(g.X - 1, g.Y));

                // Cache route count for grid and its mirror
                RouteCounts.Add(g, routes);
                if (g.X != g.Y)
                    RouteCounts.Add(new Grid(g.Y, g.X), routes);
            }

            return RouteCounts[g];
        }
    }
}
