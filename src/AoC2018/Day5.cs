using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2018
{
    public class Day5
    {
        public int Part1(string input)
        {
            var polymer = ParseInput(input);

            ReactPolymer(polymer);

            return polymer.Units.Count;
        }

        public int Part2(string input)
        {
            var polymer = ParseInput(input);

            var polymerTypes = new HashSet<char>(polymer.Units.Select(u => u.Type));
            var results = new Dictionary<char, int>();

            foreach (var polymerType in polymerTypes)
            {
                polymer = ParseInput(input);

                polymer.RemoveAllUnitsOfType(polymerType);

                ReactPolymer(polymer);

                results.Add(polymerType, polymer.Units.Count);
            }
            return results.OrderBy(k => k.Value).First().Value;
        }

        public void ReactPolymer(Polymer polymer)
        {
            while (!polymer.IsEof)
            {
                if (polymer.CurrentIsAttractedToNext())
                {
                    polymer.Units.RemoveRange(polymer.Position, 2);
                    if (polymer.Position > 0)
                    {
                        polymer.Position--;
                    }
                }
                else
                {
                    polymer.Position++;
                }
            }
        }

        private Polymer ParseInput(string input)
        {
            return new Polymer
            {
                Units = input.Select(c => new Unit { Char = c }).ToList()
            };
        }

        public class Polymer
        {
            public int Position { get; set; }
            public List<Unit> Units { get; set; }
            public bool IsEof => Position >= Units.Count;
            public Unit this[int index] => index < Units.Count ? Units[index] : null;
            public Unit CurrentUnit => this[Position];
            public Unit NextUnit => this[Position + 1];
            public bool CurrentIsAttractedToNext() => NextUnit != null ? CurrentUnit.IsAttractedTo(NextUnit) : false;
            public void RemoveAllUnitsOfType(char c) => Units = Units.Where(u => u.Type != c).ToList();
        }

        public class Unit
        {
            public char Char { get; set; }
            public bool Polarity => char.IsUpper(Char);
            public char Type => char.ToLowerInvariant(Char);
            public bool IsAttractedTo(Unit u) => u.Type == Type && u.Polarity != Polarity;
            public override string ToString() => Char.ToString();
        }
    }
}
