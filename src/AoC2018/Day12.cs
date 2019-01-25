using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2018
{
    public class Day12
    {
        public static long Part1(string input, int generations)
        {
            var plants = ParseInput(input);

            return plants.Generate(generations);
        }

        public static long Part2(string input, long generations)
        {
            var plants = ParseInput(input);

            return plants.Generate(generations);
        }

        static Garden ParseInput(string input)
        {
            var lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var state = lines[0].Substring(15)
                .Select(c => c == '#')
                .ToArray();

            var patterns = lines.Skip(1)
                .Select(l => new SpreadPattern(l));

            const int PADDING = 200;

            var padding = Enumerable.Range(0, PADDING).Select(i => false).ToArray();

            return new Garden(PADDING)
            {
                Potteries = new BitArray(padding.Concat(state).Concat(padding).ToArray()),
                SpreadPatterns = patterns.ToList()
            };
        }

        public class Garden
        {
            public Garden(int zeroIndex)
            {
                ZeroIndex = zeroIndex;
            }

            public long ZeroIndex { get; private set; }

            public BitArray Potteries { get; set; }

            public List<SpreadPattern> SpreadPatterns { get; set; }

            private long _previousDifference = int.MinValue;
            private long _previousSum = int.MinValue;

            public long GetSum() => Potteries.Cast<bool>().Select((b, i) => b ? i - ZeroIndex : 0).Sum();

            public long Generate(long generations)
            {
                for (long generation = 0; generation < generations; generation++)
                {
                    var newGeneration = new BitArray(Potteries.Length);
                    foreach (var i in Enumerable.Range(0, Potteries.Length - 5))
                    {
                        var part = Potteries.CopySlice(i, 5);
                        foreach (var pattern in SpreadPatterns)
                        {
                            if (part.IsEqualTo(pattern.Pots))
                            {
                                newGeneration[i + 2] = pattern.Outcome;
                            }
                        }
                    }
                    Potteries = newGeneration;

                    var sum = GetSum();
                    var sumDifference = sum - _previousSum;

                    if (_previousDifference == sumDifference)
                    {
                        return sum + (generations - generation - 1) * sumDifference;
                    }

                    _previousSum = sum;
                    _previousDifference = sumDifference;
                }

                return GetSum();
            }
            public override string ToString() => new string(Potteries.Cast<bool>().Select(b => b ? '#' : '.').ToArray());
        }

        public class SpreadPattern
        {
            public SpreadPattern(string pattern)
            {
                Pattern = pattern;
                Pots = new BitArray(pattern.Select(c => c == '#').Take(5).ToArray());
                Outcome = pattern[9] == '#';
            }
            public string Pattern { get; set; }
            public BitArray Pots { get; }
            public bool Outcome { get; }
        }
    }

    public static class BitArrayExtensions
    {
        public static BitArray CopySlice(this BitArray source, int offset, int length)
        {
            var ret = new BitArray(length);
            for (int i = 0; i < length; i++)
            {
                ret[i] = source[offset + i];
            }
            return ret;
        }
        public static bool IsEqualTo(this BitArray source, BitArray target)
        {
            return source[0] == target[0]
                && source[1] == target[1]
                && source[2] == target[2]
                && source[3] == target[3]
                && source[4] == target[4];
        }
    }
}
