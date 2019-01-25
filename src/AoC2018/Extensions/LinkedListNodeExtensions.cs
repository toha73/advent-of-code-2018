using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2018.Extensions
{
    public static class LinkedListNodeExtensions
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
