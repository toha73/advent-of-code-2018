using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2018
{
    public class Day6
    {
        public int Part1(string input)
        {
            var grid = ParseInput(input);

            FillGridLocations(grid);

            var infiniteCoords = GetBoundaryCoordinates(grid);

            var groupedLocations = grid.Locations
                .Where(p => p.Value != null)
                .GroupBy(p => p.Value)
                .OrderByDescending(g => g.Count())
                .Where(g => !infiniteCoords.Contains(g.Key))
                .ToList();

            return groupedLocations.First().Count();
        }

        public int Part2(string input, int maxTotalDistance)
        {
            var grid = ParseInput(input);

            FillGridLocations(grid);

            return grid.TotalDistances.Where(k => k.Value < maxTotalDistance).Count();
        }

        private Grid ParseInput(string input)
        {
            var locations = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Split(','))
                .Select(x => (int.Parse(x[0]), int.Parse(x[1])))
                .ToList();
            return new Grid(locations);
        }

        private static void FillGridLocations(Grid grid)
        {
            foreach (var location in grid.AllLocations())
            {
                var distances = grid.Coordinates
                    .Select(c => new { Coordinate = c, Distance = c.GetDistanceTo(location) })
                    .OrderBy(x => x.Distance)
                    .ToList();
                grid.TotalDistances[location] = distances.Sum(d => d.Distance);

                if (!grid.Locations.ContainsKey(location))
                {
                    var closest = distances[0].Distance == distances[1].Distance
                        ? null
                        : distances[0].Coordinate;
                    grid.Locations[location] = closest;
                }
            }
        }

        private static List<Coordinate> GetBoundaryCoordinates(Grid grid)
        {
            return grid.BoundaryLocations()
                .Select(p => grid.Get(p))
                .Distinct()
                .ToList();
        }

        public class Grid
        {
            public Grid(IEnumerable<(int x, int y)> coordinates)
            {
                Coordinates = coordinates
                    .Select((c, i) => new Coordinate(i, c))
                    .ToList();
                Coordinates.ForEach(c => Locations[c.Location] = c);
                MinX = Coordinates.Min(p => p.Location.X);
                MaxX = Coordinates.Max(p => p.Location.X);
                MinY = Coordinates.Min(p => p.Location.Y);
                MaxY = Coordinates.Max(p => p.Location.Y);
                XLocations = Enumerable.Range(MinX, MaxX - MinX + 1).ToList();
                YLocations = Enumerable.Range(MinY, MaxY - MinY + 1).ToList();
            }

            public int MinX { get; }
            public int MaxX { get; }
            public int MinY { get; }
            public int MaxY { get; }
            public List<int> XLocations { get; }
            public List<int> YLocations { get; }
            public List<Coordinate> Coordinates { get; }
            public Dictionary<(int x, int y), int> TotalDistances { get; } = new Dictionary<(int x, int y), int>();
            public Dictionary<(int x, int y), Coordinate> Locations { get; } = new Dictionary<(int x, int y), Coordinate>();
            public Coordinate Get((int x, int y) location) => Locations.TryGetValue(location, out var coordinate) ? coordinate : default;
            public IEnumerable<(int x, int y)> AllLocations() => YLocations.SelectMany(y => XLocations.Select(x => (x, y)));
            public IEnumerable<(int x, int y)> BoundaryLocations() =>
                XLocations
                    .Select(x => (x, MinY))
                    .Concat(XLocations.Select(x => (x, MaxY)))
                    .Concat(YLocations.Select(y => (MinX, y + 1)))
                    .Concat(YLocations.Select(y => (MaxX, y - 1)))
                    .ToList();
        }

        public class Coordinate : IEquatable<Coordinate>
        {
            public Coordinate(int id, (int x, int y) location)
            {
                Id = id;
                Location = location;
            }
            public int Id { get; }
            public (int X, int Y) Location { get; }
            public int GetDistanceTo((int x, int y) p) => Math.Abs(Location.X - p.x) + Math.Abs(Location.Y - p.y);
            public override int GetHashCode() => (Id, Location).GetHashCode();
            public override string ToString() => $"{Id}:{Location}";
            public bool Equals(Coordinate other) => other.GetHashCode() == GetHashCode();
        }
    }
}
