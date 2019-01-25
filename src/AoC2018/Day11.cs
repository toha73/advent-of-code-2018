using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2018
{
    public class Day11
    {
        public static string Part1(int serialNumber)
        {
            var grid = GetGrid(serialNumber);

            var result = grid.GetSquareMaxResult(3);

            return $"{result.X},{result.Y}";
        }

        public static string Part2(int serialNumber)
        {
            var grid = GetGrid(serialNumber);

            foreach (var i in Enumerable.Range(2, 300))
            {
                grid.CalculatePowerGridLevels(i);
            }

            var top = grid.MaxResults.OrderByDescending(r => r.Value.Level).Take(10);
            var first = top.First();

            return $"{first.Value.X},{first.Value.Y},{first.Key}";
        }

        public static int GetHighestSquarePowerLevel(int serialNumber)
        {
            var grid = GetGrid(serialNumber);

            return grid.GetSquareMaxResult(3).Level;
        }

        private static Grid GetGrid(int serialNumber)
        {
            var cells = Enumerable.Range(0, 300 * 300)
                .Select(i => (i % 300 + 1, i / 300 + 1))
                .ToDictionary(f => f, f => new FuelCell { Position = f, PowerLevel = GetPowerLevel(f, serialNumber) });

            return new Grid { FuelCells = cells };
        }

        public static int GetPowerLevel((int X, int Y) position, int serialNumber)
        {
            var rackId = position.X + 10;
            var p = (rackId * position.Y + serialNumber) * rackId;
            var z = p > 100
                ? p / 100 % 10
                : 0;
            return z - 5;
        }

        public class Grid
        {
            public Dictionary<(int X, int Y), FuelCell> FuelCells { get; set; }
            public Dictionary<int, MaxResult> MaxResults { get; set; } = new Dictionary<int, MaxResult>();

            public void CalculatePowerGridLevels(int squareSize)
            {
                var columnAmount = new Dictionary<(int, int), int>();   // column starting at position and squareSize height.

                if (MaxResults.ContainsKey(squareSize)) return;
                var maxResult = new MaxResult();
                foreach (var y in Enumerable.Range(1, 301 - squareSize))
                {
                    columnAmount.Clear();
                    foreach (var x in Enumerable.Range(1, 300))
                    {
                        var columnValue = Enumerable.Range(y, squareSize).Sum(y2 => FuelCells[(x, y2)].PowerLevel);
                        columnAmount[(x, y)] = columnValue;
                    }
                    foreach (var x in Enumerable.Range(1, 301 - squareSize))
                    {
                        var level = Enumerable.Range(x, squareSize).Sum(x2 => columnAmount[(x2, y)]);
                        if (level > maxResult.Level)
                        {
                            maxResult = new MaxResult { X = x, Y = y, Level = level };
                        }
                    }
                }
                MaxResults.Add(squareSize, maxResult);
            }


            public MaxResult GetSquareMaxResult(int squareSize)
            {
                CalculatePowerGridLevels(squareSize);

                return MaxResults[squareSize];
            }
        }

        public struct MaxResult
        {
            public int X;
            public int Y;
            public int Level;
        }

        public class FuelCell
        {
            public (int X, int Y) Position { get; set; }
            public int PowerLevel { get; set; }
            public override string ToString() => Position.ToString();
            public static FuelCell Create(int x, int y) => new FuelCell { Position = (x, y) };
            public static FuelCell Create((int x, int y) position) => new FuelCell { Position = position };
        }
    }
}
