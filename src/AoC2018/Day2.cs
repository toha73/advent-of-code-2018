using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2018
{
    public class Day2
    {
        private List<Data> _boxes;

        public int Part1(string input)
        {
            ParseInput(input);

            var aggregated = _boxes
                .Select(b => b.Characters.GroupBy(c => c).ToList())
                .Select(g => new
                {
                    Twos = g.Where(gg => gg.Count() == 2).Count() > 0,
                    Threes = g.Where(gg => gg.Count() == 3).Count() > 0
                })
                .ToList();

            return aggregated.Where(x => x.Twos).Count() * aggregated.Where(x => x.Threes).Count();
        }

        public string Part2(string input)
        {
            ParseInput(input);

            var result = _boxes
                .SelectMany((b, i) => _boxes.Skip(i)
                    .Select(bb => new { b1 = b, b2 = bb })
                    .Where(x => x.b1 != x.b2))
                .Select(x => new { x.b1, x.b2, score = GetSimilarityScore(x.b2, x.b1) })
                .OrderBy(x => x.score)
                .ToList();

            return GetCommonString(result[0].b1, result[0].b2);
        }

        private int GetSimilarityScore(Data d1, Data d2) => d1.Numbers.Zip(d2.Numbers, (i1, i2) => i2 - i1).Where(n => n != 0).Count();

        private string GetCommonString(Data d1, Data d2)
        {
            return new string(
                d1.Numbers
                    .Zip(d2.Numbers, (i1, i2) => i2 - i1)
                    .Select((n, i) => new { n, c = d1.Value[i] })
                    .Where(x => x.n == 0)
                    .Select(x => x.c)
                    .ToArray());
        }

        private void ParseInput(string input)
        {
            _boxes = input
                .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new Data { Value = x, Characters = x.ToList(), Numbers = x.Select(c => (int)c).ToList() })
                .ToList();
        }
    }

    public class Data
    {
        public string Value { get; set; }
        public List<char> Characters { get; set; }
        public List<int> Numbers { get; set; }
        public override string ToString() => Value;

    }
}
