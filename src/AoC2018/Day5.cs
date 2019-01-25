using Advent_of_Code_2018.Extensions;
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
                    polymer.RemoveCurrent(2);
                    polymer.Back();
                }
                else
                {
                    polymer.Forward();
                }
            }
        }

        private Polymer ParseInput(string input)
        {
            return new Polymer(input.Select(c => new Unit { Char = c }));
        }

        public class Polymer
        {
            private LinkedListNode<Unit> _currentNode;

            public Polymer(IEnumerable<Unit> units)
            {
                Units = new LinkedList<Unit>(units);
                _currentNode = Units.First;
            }
            public LinkedList<Unit> Units { get; }
            public bool IsEof => _currentNode == null;
            public Unit CurrentUnit => _currentNode.Value;
            public Unit NextUnit => _currentNode.Next?.Value;
            public void Forward() => _currentNode = _currentNode.Next;
            public void Back() => _currentNode = _currentNode.Previous ?? _currentNode;
            public bool CurrentIsAttractedToNext() => NextUnit != null ? CurrentUnit.IsAttractedTo(NextUnit) : false;
            public void RemoveCurrent(int repeat = 1)
            {
                var node = _currentNode;
                foreach (var i in Enumerable.Range(0, repeat))
                {
                    var nextNode = node.Next ?? node.Previous;
                    Units.Remove(node);
                    node = nextNode;
                }
                _currentNode = node;
            }
            public void RemoveAllUnitsOfType(char c)
            {
                foreach (var node in Units.Nodes())
                {
                    if (node.Value.Type == c)
                    {
                        Units.Remove(node);
                    }
                }
            }
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
