using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2018
{
    public class Day8
    {
        public static int Part1(string input)
        {
            var rootNode = ParseInput(input);

            return rootNode.DescendantsAndSelf().Sum(n => n.MetaData.Sum());
        }

        public static int Part2(string input)
        {
            var rootNode = ParseInput(input);

            return rootNode.GetValue();
        }

        public static Node ParseInput(string input)
        {
            var i = 0;
            var data = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0];
            var headers = data.Split(' ').Select(s => int.Parse(s)).ToList();

            Node ReadNode()
            {
                var node = new Node
                {
                    ChildCount = headers[i + 0],
                    MetaDataCount = headers[i + 1],
                };
                i += 2;
                node.Children = Enumerable.Range(0, node.ChildCount).Select(j => ReadNode()).ToList();
                node.MetaData = Enumerable.Range(i, node.MetaDataCount).Select(j => headers[j]).ToList();
                i += node.MetaDataCount;
                return node;
            }

            return ReadNode();
        }

        public class Node
        {
            private static int _idCount = 0;
            public int Id { get; } = _idCount++;
            public int ChildCount { get; set; }
            public int MetaDataCount { get; set; }
            public List<Node> Children { get; set; }
            public List<int> MetaData { get; set; }
            public IEnumerable<Node> DescendantsAndSelf()
            {
                yield return this;
                foreach (var child in Children)
                {
                    foreach (var descendant in child.DescendantsAndSelf())
                    {
                        yield return descendant;
                    }
                }
            }
            public int GetValue() => ChildCount == 0
                ? MetaData.Sum()
                : MetaData.Sum(i => Children.ElementAtOrDefault(i - 1)?.GetValue() ?? 0);
            public override string ToString() => $"{Id}";
        }
    }
}
