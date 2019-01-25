using System.Collections.Generic;

namespace Advent_of_Code_2018.Extensions
{
    public static class LinkedListExtensions
    {
        public static IEnumerable<LinkedListNode<T>> Nodes<T>(this LinkedList<T> list)
        {
            var node = list.First;
            while (node != null)
            {
                var nextNode = node.Next;
                yield return node;
                node = nextNode;
            }
        }
    }
}
