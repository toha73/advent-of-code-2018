using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2018
{
    public class Day13
    {
        public static string Part1(string input)
        {
            var map = ParseInput(input);

            var (X, Y) = map.RunCartsUntilFirstCrash();

            return $"{X},{Y}";
        }

        public static string Part2(string input)
        {
            var map = ParseInput(input);

            var lastCartPosition = map.RunCartsOneCartRemain();

            return $"{lastCartPosition.X},{lastCartPosition.Y}";
        }

        public static Map ParseInput(string input)
        {
            var lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var tracks = lines
                .SelectMany((l, y) => l
                    .Select((c, x) => new Track { Position = (x, y), Char = c })
                    .Where(t => t.Char != ' '))
                .ToList();

            var cartChars = new[] { 'v', '^', '<', '>' };
            var cartMappings = new Dictionary<char, char>
            {
                { 'v', '|' },
                { '^', '|' },
                { '<', '-' },
                { '>', '-' },
            };

            var carts = tracks
                .Where(t => cartChars.Contains(t.Char))
                .Select(c => new Cart(c.Char) { Position = c.Position })
                .ToList();

            tracks.ForEach(t => t.Char = cartMappings.ContainsKey(t.Char) ? cartMappings[t.Char] : t.Char);

            return new Map
            {
                Tracks = tracks.ToDictionary(t => t.Position),
                Carts = carts
            };
        }

        public class Map
        {
            public Dictionary<(int X, int Y), Track> Tracks { get; set; } = new Dictionary<(int X, int Y), Track>();
            public List<Cart> Carts { get; set; } = new List<Cart>();
            public IEnumerable<Cart> OrderedCarts => Carts.OrderBy(c => c.Position.Y).ThenBy(c => c.Position.X);

            public (int X, int Y) RunCartsUntilFirstCrash()
            {
                foreach (var i in Enumerable.Range(0, 10000))
                {
                    foreach (var cart in OrderedCarts)
                    {
                        cart.Move(this);

                        if (GetCartsAtPosition(cart.Position).Count > 1)
                        {
                            return cart.Position;
                        }
                    }
                }
                throw new InvalidOperationException();
            }

            public (int X, int Y) RunCartsOneCartRemain()
            {
                foreach (var i in Enumerable.Range(0, 100000))
                {
                    foreach (var cart in OrderedCarts)
                    {
                        cart.Move(this);

                        var cartsAtPosition = GetCartsAtPosition(cart.Position);
                        if (cartsAtPosition.Count > 1)
                        {
                            cartsAtPosition.ForEach(c => c.HasCrashed = true);
                        }
                    }

                    var uncrashedCarts = Carts.Where(c => !c.HasCrashed).ToList();
                    if (uncrashedCarts.Count == 1)
                    {
                        return uncrashedCarts.First().Position;
                    }
                }
                throw new InvalidOperationException();
            }

            public List<Cart> GetCartsAtPosition((int X, int Y) position) => Carts.Where(c => !c.HasCrashed && c.Position == position).ToList();

            private List<(int X, int Y)> GetCollisions()
            {
                return Carts
                    .GroupBy(g => g.Position)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();
            }
        }

        public class Cart
        {
            public Cart(char c)
            {
                Char = c;
            }
            public (int X, int Y) Position;
            public char Char;
            public bool HasCrashed;
            public int TurnCount;
            public override string ToString() => $"{Char} <{Position}>";
            public (int X, int Y) GetNextPosition()
            {
                switch (Char)
                {
                    case '<': return (Position.X - 1, Position.Y);
                    case '>': return (Position.X + 1, Position.Y);
                    case '^': return (Position.X, Position.Y - 1);
                    case 'v': return (Position.X, Position.Y + 1);
                }
                throw new ArgumentException();
            }

            public void Move(Map map)
            {
                if (HasCrashed) return;
                var nextPosition = GetNextPosition();
                var track = map.Tracks[nextPosition];
                Char = _cartRoutes[Char][track.Char];
                Position = nextPosition;
                if (track.Char == '+')
                {
                    var direction = TurnCount % 3;
                    if (direction != 1)
                    {
                        Char = _cartTurnRoutes[direction][Char];
                    }
                    TurnCount++;
                }
            }

            private static readonly Dictionary<char, Dictionary<char, char>> _cartRoutes = new Dictionary<char, Dictionary<char, char>>
            {
                {
                    '<',
                    new Dictionary<char, char>
                    {
                        { '-', '<' },
                        { '+', '<' },
                        { '/', 'v' },
                        { '\\', '^' },
                    }
                },
                {
                    '>',
                    new Dictionary<char, char>
                    {
                        { '-', '>' },
                        { '+', '>' },
                        { '/', '^' },
                        { '\\', 'v' },
                    }
                },
                {
                    '^',
                    new Dictionary<char, char>
                    {
                        { '|', '^' },
                        { '+', '^' },
                        { '/', '>' },
                        { '\\', '<' },
                    }
                },
                {
                    'v',
                    new Dictionary<char, char>
                    {
                        { '|', 'v' },
                        { '+', 'v' },
                        { '/', '<' },
                        { '\\', '>' },
                    }
                },
            };

            private static readonly Dictionary<int, Dictionary<char, char>> _cartTurnRoutes = new Dictionary<int, Dictionary<char, char>>
            {
                {
                    0,
                    new Dictionary<char, char>
                    {
                        { '<', 'v' },
                        { 'v', '>' },
                        { '>', '^' },
                        { '^', '<' },
                    }
                },
                {
                    2,
                    new Dictionary<char, char>
                    {
                        { '<', '^' },
                        { '^', '>' },
                        { '>', 'v' },
                        { 'v', '<' },
                    }
                },
            };
        }

        public class Track
        {
            public (int X, int Y) Position;
            public char Char;
            public override string ToString() => Char.ToString();
        }
    }
}
