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

    public static class Extensions
    {
        public static LinkedListNode<T> Forward<T>(this LinkedListNode<T> node, int count)
        {
            if (count == 0) return node;
            var n = node;
            foreach (var i in Enumerable.Range(0, count))
            {
                n = n.Next ?? n.List.First;
            }
            return n;
        }
        public static LinkedListNode<T> Backward<T>(this LinkedListNode<T> node, int count)
        {
            if (count == 0) return node;
            var n = node;
            if (count > 0)
            {
                foreach (var i in Enumerable.Range(0, count))
                {
                    n = n.Previous ?? n.List.Last;
                }
            }
            return n;
        }
    }
}
