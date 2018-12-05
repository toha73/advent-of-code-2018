using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2018
{
    public class Day1
    {
        private List<int> _numbers;

        public int Part1(string input)
        {
            ParseInput(input);

            return _numbers.Aggregate((x, y) => x + y);
        }

        public int Part2(string input)
        {
            ParseInput(input);

            var frequencies = new HashSet<int>();
            var frequency = 0;
            while (true)
            {
                foreach (var n in _numbers)
                {
                    frequency += n;
                    if (frequencies.Contains(frequency))
                    {
                        return frequency;
                    }
                    frequencies.Add(frequency);
                }
            }
        }

        private void ParseInput(string input)
        {
            _numbers = input.Split(new[] { '\n', '\r', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s))
                .ToList();
        }

    }
}
