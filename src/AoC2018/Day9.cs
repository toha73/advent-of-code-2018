﻿using Advent_of_Code_2018.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2018
{
    public class Day9
    {
        public static long Part1And2(int players, int lastMarbleWorth)
        {
            var marbles = new LinkedList<int>();
            var currentMarble = marbles.AddFirst(0);
            var playerScores = Enumerable.Range(1, players).Select(i => (long)0).ToList();
            foreach (var marble in Enumerable.Range(1, lastMarbleWorth))
            {
                if (marble % 23 == 0)
                {
                    var player = marble % players;
                    currentMarble = currentMarble.Backward(7);
                    playerScores[player] += marble + (long)currentMarble.Value;
                    currentMarble = currentMarble.Forward(1);
                    currentMarble.List.Remove(currentMarble.Backward(1));
                }
                else
                {
                    currentMarble = currentMarble.Forward(1);
                    currentMarble = currentMarble.List.AddAfter(currentMarble, marble);
                }
            }
            return playerScores.Max();
        }
    }
}
