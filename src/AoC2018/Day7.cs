using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2018
{
    public class Day7
    {
        public static string Part1(string input)
        {
            var nodes = ParseInput(input);

            var result = new List<char>();
            while (nodes.ExistingNodes.Count > 0)
            {
                var readyNode = nodes.ReadyNodes.First();

                nodes.MarkNodeAsReady(readyNode.Id);

                result.Add(readyNode.Id);
            }

            return new string(result.ToArray());
        }


        public static int Part2(string input, int workerCount, int extraDuration)
        {
            var nodes = ParseInput(input);

            var occupiedWorkers = new List<WorkOrder>();

            int time = 0;
            while (nodes.ExistingNodes.Count > 0)
            {
                var freeWorkers = workerCount - occupiedWorkers.Count;

                var workOrders = nodes.ReadyNodes
                    .Where(x => !occupiedWorkers.Select(w => w.Node.Id).Contains(x.Id))
                    .Take(freeWorkers)
                    .Select(n => new WorkOrder { Node = n, RemainingTime = n.Weight + extraDuration })
                    .ToList();

                // Assign workers
                occupiedWorkers.AddRange(workOrders);

                // Work on nodes and add time
                var workTime = occupiedWorkers.Min(w => w.RemainingTime);
                time += workTime;
                occupiedWorkers.ForEach(w => w.RemainingTime -= workTime);

                // Remove finished work orders
                var finishedWorkers = occupiedWorkers.Where(w => w.IsFinished).ToList();
                var finishedNodes = finishedWorkers.Select(w => w.Node).ToList();
                occupiedWorkers = occupiedWorkers.Except(finishedWorkers).ToList();

                // Mark nodes as finished
                foreach (var readyNode in finishedNodes)
                {
                    nodes.MarkNodeAsReady(readyNode.Id);
                }
            }

            return time;
        }

        private static NodeSet ParseInput(string input)
        {
            var relations = new List<(char Dependency, char Id)>();
            relations = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => Regex.Match(l, "Step (.*) must be finished before step (.*) can begin."))
                .Select(m => (m.Groups[1].Value.First(), m.Groups[2].Value.First()))
                .ToList();

            var existingNodes = new HashSet<char>(
                relations
                    .SelectMany(x => new[] { x.Dependency, x.Id })
                    .Distinct());

            var nodes = existingNodes
                .Select(n => new Node
                {
                    Id = n,
                    Dependencies = new HashSet<char>(relations.Where(r => r.Id == n).Select(r => r.Dependency))
                })
                .ToDictionary(n => n.Id);

            return new NodeSet
            {
                ExistingNodes = existingNodes,
                Nodes = nodes
            };
        }

        public class NodeSet
        {
            public HashSet<char> ExistingNodes { get; set; }
            public Dictionary<char, Node> Nodes { get; set; }
            public void MarkNodeAsReady(char id)
            {
                ExistingNodes.Remove(id);
                Nodes.Remove(id);
                foreach (var childNode in Nodes.Values)
                {
                    childNode.Dependencies.Remove(id);
                }
            }
            public IEnumerable<Node> ReadyNodes => Nodes.Values.Where(x => x.Dependencies.Count == 0).OrderBy(x => x.Id);
        }

        public class Node
        {
            public char Id { get; set; }
            public HashSet<char> Dependencies { get; set; } = new HashSet<char>();
            public override string ToString() => $"{Id}";
            public int Weight => Id - 'A' + 1;
        }

        public class WorkOrder
        {
            public Node Node { get; set; }
            public int RemainingTime { get; set; }
            public bool IsFinished => RemainingTime == 0;
        }
    }
}
