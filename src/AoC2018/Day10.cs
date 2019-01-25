using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2018
{
    public class Day10
    {
        public static Bitmap Part1(string input)
        {
            var points = ParseInput(input);

            MovePoints(ref points);

            return Paint(points);
        }

        public static int Part2(string input)
        {
            var points = ParseInput(input);

            return MovePoints(ref points);
        }

        public static int MovePoints(ref List<Point> points)
        {
            foreach (var iteration in Enumerable.Range(1, 100000))
            {
                foreach (var point in points)
                {
                    point.Position += point.Velocity;
                }

                if (AreAllAdjacentToOthers(points))
                {
                    Paint(points);
                    return iteration;
                }
            }
            throw new Exception("Nope!");
        }


        public static Bitmap Paint(List<Point> points)
        {
            var zoom = 1f;
            const int padding = 4;
            var left = points.Min(p => p.Position.X);
            var width = points.Max(p => p.Position.X) - left;
            var top = points.Min(p => p.Position.Y);
            var height = points.Max(p => p.Position.Y) - top;

            var bitmap = new Bitmap((int)(width * zoom) + 1 + padding, (int)(height * zoom) + 1 + padding);
            foreach (var point in points)
            {
                bitmap.SetPixel(
                    (int)((point.Position.X - left) * zoom) + padding / 2,
                    (int)((point.Position.Y - top) * zoom) + padding / 2,
                    Color.Red);
            }
            return bitmap;
        }

        private static bool AreAllAdjacentToOthers(List<Point> points)
        {
            var checkedPositions = new HashSet<Position>();
            for (var i = 0; i < points.Count - 1; i++)
            {
                if (checkedPositions.Contains(points[i].Position)) continue;

                var adjacentPoints = points.Skip(i + 1).Where(p => p.IsAdjacentTo(points[i])).ToList();
                if (adjacentPoints.Count == 0)
                {
                    return false;
                }
                checkedPositions.Add(points[i].Position);
                checkedPositions.UnionWith(adjacentPoints.Select(p => p.Position));
            }
            return true;
        }

        public static List<Point> ParseInput(string input)
        {
            return input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => Regex.Match(l, @"position=<\s*?(-?\d*),\s*?(-?\d*)> velocity=<\s*?(-?\d*),\s*?(-?\d*)>"))
                .Select(m => new Point
                {
                    Position = new Position(int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value)),
                    Velocity = new Position(int.Parse(m.Groups[3].Value), int.Parse(m.Groups[4].Value)),
                })
                .ToList();
        }



        public class Point
        {
            public Position Position { get; set; }
            public Position Velocity { get; set; }
            public bool IsAdjacentTo(Point p) => p.Position.IsAdjacentTo(Position);
            public override string ToString() => Position.ToString();
        }

        public struct Position
        {
            public Position(int x, int y)
            {
                X = x; Y = y;
            }
            public int X { get; set; }
            public int Y { get; set; }
            public bool IsEmpty => X == 0 && Y == 0;
            public override string ToString() => $"<{X},{Y}>";
            public bool IsAdjacentTo(Position p)
            {
                var d = this - p;
                return !d.IsEmpty
                    && d.X <= 1 && d.X >= -1
                    && d.Y <= 1 && d.Y >= -1;
            }
            public static Position operator +(Position p1, Position p2) => new Position { X = p1.X + p2.X, Y = p1.Y + p2.Y };
            public static Position operator -(Position p1, Position p2) => new Position { X = p1.X - p2.X, Y = p1.Y - p2.Y };
        }
    }
}
