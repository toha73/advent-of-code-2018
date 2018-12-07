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

            FillGridPositions(grid);

            var infiniteCoords = GetBoundaryCoordinates(grid);

            var groupedPositions = grid.Positions
                .Where(p => p.Value != null)
                .GroupBy(p => p.Value)
                .OrderByDescending(g => g.Count())
                .Where(g => !infiniteCoords.Contains(g.Key))
                .ToList();

            return groupedPositions.First().Count();

        }

        private Grid ParseInput(string input)
        {
            var positions = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Split(','))
                .Select(x => (int.Parse(x[0]), int.Parse(x[1])))
                .ToList();
            return new Grid(positions);
        }

        private static void FillGridPositions(Grid grid)
        {
            foreach (var position in grid.AllPositions())
            {
                if (!grid.Positions.ContainsKey(position))
                {
                    var closest = grid.GetClosestCoordinateTo(position);
                    grid.Positions[position] = closest;
                }
            }
        }

        private static List<Coordinate> GetBoundaryCoordinates(Grid grid)
        {
            return grid.BoundaryPositions()
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
                Coordinates.ForEach(c => Positions[c.Position] = c);
                MinX = Coordinates.Min(p => p.Position.X);
                MaxX = Coordinates.Max(p => p.Position.X);
                MinY = Coordinates.Min(p => p.Position.Y);
                MaxY = Coordinates.Max(p => p.Position.Y);
                XPositions = Enumerable.Range(MinX, MaxX - MinX + 1).ToList();
                YPositions = Enumerable.Range(MinY, MaxY - MinY + 1).ToList();
            }

            public int MinX { get; }
            public int MaxX { get; }
            public int MinY { get; }
            public int MaxY { get; }
            public List<int> XPositions { get; }
            public List<int> YPositions { get; }
            public List<Coordinate> Coordinates { get; }
            public Dictionary<(int x, int y), Coordinate> Positions { get; } = new Dictionary<(int x, int y), Coordinate>();
            public Coordinate Get((int x, int y) position) => Positions.TryGetValue(position, out var coordinate) ? coordinate : default;
            public Coordinate GetClosestCoordinateTo((int x, int y) position)
            {
                var closest = Coordinates
                    .Select(c => new { Coordinate = c, Distance = c.GetDistanceTo(position) })
                    .OrderBy(x => x.Distance)
                    .Take(2)
                    .ToList();
                return closest[0].Distance == closest[1].Distance ? null : closest[0].Coordinate;
            }
            public IEnumerable<(int x, int y)> AllPositions() => YPositions.SelectMany(y => XPositions.Select(x => (x, y)));
            public IEnumerable<(int x, int y)> BoundaryPositions() => 
                XPositions
                    .Select(x => (x, MinY))
                    .Concat(XPositions.Select(x => (x, MaxY)))
                    .Concat(YPositions.Select(y => (MinX, y + 1)))
                    .Concat(YPositions.Select(y => (MaxX, y - 1)))
                    .ToList();
        }

        public class Coordinate : IEquatable<Coordinate>
        {
            public Coordinate(int id, (int x, int y) position)
            {
                Id = id;
                Position = position;
            }
            public int Id { get; }
            public (int X, int Y) Position { get; }
            public int GetDistanceTo((int x, int y) p) => Math.Abs(Position.X - p.x) + Math.Abs(Position.Y - p.y);
            public override int GetHashCode() => (Id, Position).GetHashCode();
            public override string ToString() => $"{Id}:{Position}";
            public bool Equals(Coordinate other) => other.GetHashCode() == GetHashCode();
        }
    }
}
