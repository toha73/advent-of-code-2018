using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2018
{
    public class Day1
    {
        private List<int> _numbers;

        public int Solution(string input)
        {
            ParseInput(input);

            return _numbers.Aggregate((x, y) => x + y);
        }

        private void ParseInput(string input)
        {
            _numbers = input.Split(new[] { '\n', '\r', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s))
                .ToList();
        }

    }
}
