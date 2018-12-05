using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2018
{
    public class Day3
    {
        private Dictionary<(int x, int y), List<int>> _canvas1 = new Dictionary<(int x, int y), List<int>>();
        private Dictionary<(int x, int y), HashSet<int>> _canvas2 = new Dictionary<(int x, int y), HashSet<int>>();
        private List<Data> _claims;

        public int Part1(string input)
        {
            ParseInput(input);

            foreach (var claim in _claims)
            {
                foreach (var p in claim.Positions)
                {
                    if (!_canvas1.ContainsKey(p)) _canvas1[p] = new List<int>();
                    _canvas1[p].Add(claim.Id);
                }
            }

            return _canvas1.Where(k => k.Value.Count > 1).Count();
        }

        public int Part2(string input)
        {
            ParseInput(input);

            foreach (var claim in _claims)
            {
                foreach (var p in claim.Positions)
                {
                    if (!_canvas2.ContainsKey(p)) _canvas2[p] = new HashSet<int>();
                    _canvas2[p].Add(claim.Id);
                }
            }

            var result = _claims
                .Select(c => new { Claim = c, SinglePositions = c.Positions.Select(p => _canvas2[p]).Where(x => x.Count > 1).ToList() })
                .Where(x => x.SinglePositions.Count == 0);

            return result.First().Claim.Id;
        }


        private void ParseInput(string input)
        {
            _claims = input
                .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split(new[] { '@', ',', ':', 'x' }))
                .Select(a => new Data
                {
                    Id = int.Parse(a[0].TrimStart('#')),
                    Position = (int.Parse(a[1]), int.Parse(a[2])),
                    Size = (int.Parse(a[3]), int.Parse(a[4]))
                })
                .ToList();
        }

        public class Data
        {
            public int Id { get; set; }
            public (int x, int y) Position { get; set; }
            public (int width, int height) Size { get; set; }
            public IEnumerable<(int x, int y)> Positions =>
                Enumerable.Range(Position.x, Size.width)
                    .SelectMany(x =>
                        Enumerable.Range(Position.y, Size.height),
                        (int x, int y) => (x, y));
        }
    }
}
