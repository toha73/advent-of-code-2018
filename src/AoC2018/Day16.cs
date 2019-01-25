using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2018
{
    public class Day16
    {
        public static int Part1(string input)
        {
            var instructions = ParseInput(input);

            var samples = instructions
                .TakeWhile(i => i.Before != null)
                .ToList();

            return samples
                .Select(s => new
                {
                    Sample = s,
                    PossibleInstructions = s.GetPossibleInstructions().ToList()
                })
                .Where(x => x.PossibleInstructions.Count >= 3)
                .Count();
        }

        public static int Part2(string input)
        {
            var instructions = ParseInput(input);

            var mappings = GetInstructionMapping(instructions);

            var program = instructions.SkipWhile(i => i.Before != null).ToList();

            var register = new Registers();

            foreach (var instruction in program)
            {
                mappings[instruction.OpCode].Execute(instruction.Parameters, register);
            }

            return register.Values[0];
        }

        public static Dictionary<int, OpCode> GetInstructionMapping(IEnumerable<Instruction> instructions)
        {
            var samples = instructions
                .TakeWhile(i => i.Before != null)
                .GroupBy(s => s.OpCode)
                .ToList();

            var mappings = new Dictionary<int, OpCode>();

            foreach (var i in Enumerable.Range(0, 17))
            {
                var data1 = samples
                    .Where(g => !mappings.ContainsKey(g.Key))
                    .Select(g => new
                    {
                        OpCode = g.Key,
                        Samples = g
                            .Select(s => new
                            {
                                Sample = s,
                                PossibleInstructions = s.GetPossibleInstructions()
                                    .Where(p => mappings.Values.All(v => v != p))
                                    .ToList()
                            })
                            .ToList()
                    })
                    .ToList();

                var given = data1
                    .Select(x => new
                    {
                        x.OpCode,
                        Instruction = x.Samples.Where(s => s.PossibleInstructions.Count == 1).FirstOrDefault()?.PossibleInstructions.First()
                    })
                    .Where(x => x.Instruction != null)
                    .ToList();

                given.ForEach(g => mappings.Add(g.OpCode, g.Instruction));
            }

            return mappings;
        }

        public static List<Instruction> ParseInput(string input)
        {
            const string REGISTER_PATTERN = @"(Before|After):\s*?\[(\d*), (\d*), (\d*), (\d*)\]";

            var lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var instructions = new List<Instruction>();

            Instruction current = null;
            Registers register = null;
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var matches = Regex.Matches(line, REGISTER_PATTERN);
                if (matches.Count > 0)
                {
                    register = new Registers
                    {
                        Values = new int[]
                        {
                        int.Parse(matches[0].Groups[2].Value),
                        int.Parse(matches[0].Groups[3].Value),
                        int.Parse(matches[0].Groups[4].Value),
                        int.Parse(matches[0].Groups[5].Value),
                        }
                    };
                    if (current != null)
                    {
                        current.After = register;
                        register = null;
                        current = null;
                    }
                }
                else
                {
                    matches = Regex.Matches(line, @"(\d*) (\d*) (\d*) (\d*)");
                    if (matches.Count > 0)
                    {
                        current = new Instruction
                        {
                            OpCode = int.Parse(matches[0].Groups[1].Value),
                            Parameters = new Parameters
                            {
                                A = int.Parse(matches[0].Groups[2].Value),
                                B = int.Parse(matches[0].Groups[3].Value),
                                C = int.Parse(matches[0].Groups[4].Value),
                            }
                        };
                        if (register != null)
                        {
                            current.Before = register;
                        }
                        instructions.Add(current);
                    }
                }
            }
            return instructions;
        }

        public class Parameters
        {
            public Parameters() { }
            public Parameters(int a, int b, int c)
            {
                A = a; B = b; C = c;
            }
            public int A { get; set; }
            public int B { get; set; }
            public int C { get; set; }
            public override string ToString() => $"{A} {B} {C}";
        }

        public class Instruction
        {
            public int OpCode { get; set; }
            public Parameters Parameters { get; set; }
            public Registers Before { get; set; }
            public Registers After { get; set; }
            public override string ToString() => $"{OpCode} {Parameters}";
            public bool IsValidInstruction(OpCode code)
            {
                if (Before == null || After == null)
                {
                    throw new InvalidOperationException();
                }

                var r = Before.Clone();
                code.Execute(Parameters, r);
                return r.Equals(After);
            }
            public IEnumerable<OpCode> GetPossibleInstructions()
                => Cpu.CodeInstructions.Where(c => IsValidInstruction(c));
        }

        public class Cpu
        {
            public static readonly List<OpCode> CodeInstructions = new List<OpCode>
            {
                new AddR(),
                new AddI(),
                new MulR(),
                new MulI(),
                new BanR(),
                new BanI(),
                new BorR(),
                new BorI(),
                new SetR(),
                new SetI(),
                new GtRI(),
                new GtIR(),
                new GtRR(),
                new EqRI(),
                new EqIR(),
                new EqRR(),
            };
        }

        public class Registers : IEquatable<Registers>
        {
            public Registers() { }
            public Registers(params int[] values)
            {
                Values = values.Take(4).ToArray();
            }
            public int[] Values { get; set; } = new int[4];
            public int this[int i]
            {
                get => Values[i];
                set => Values[i] = value;
            }
            public bool Equals(Registers other) =>
                other != null
                && other.Values.Length == Values.Length
                && other.Values.Zip(Values, (v1, v2) => v1 == v2).All(b => b);
            public override string ToString() => string.Join(" ", Values.Select(v => v.ToString()));
            public Registers Clone() => new Registers { Values = Values.ToArray() };
        }

        public abstract class OpCode
        {
            public string Id => GetType().Name;

            public abstract void Execute(Parameters p, Registers register);
        }

        public class AddR : OpCode
        {
            public override void Execute(Parameters p, Registers register)
                => register[p.C] = register[p.A] + register[p.B];
        }
        public class AddI : OpCode
        {
            public override void Execute(Parameters p, Registers register)
                => register[p.C] = register[p.A] + p.B;
        }
        public class MulR : OpCode
        {
            public override void Execute(Parameters p, Registers register)
                => register[p.C] = register[p.A] * register[p.B];
        }
        public class MulI : OpCode
        {
            public override void Execute(Parameters p, Registers register)
                => register[p.C] = register[p.A] * p.B;
        }
        public class BanR : OpCode
        {
            public override void Execute(Parameters p, Registers register)
                => register[p.C] = register[p.A] & register[p.B];
        }
        public class BanI : OpCode
        {
            public override void Execute(Parameters p, Registers register)
                => register[p.C] = register[p.A] & p.B;
        }
        public class BorR : OpCode
        {
            public override void Execute(Parameters p, Registers register)
                => register[p.C] = register[p.A] | register[p.B];
        }
        public class BorI : OpCode
        {
            public override void Execute(Parameters p, Registers register)
                => register[p.C] = register[p.A] | p.B;
        }
        public class SetR : OpCode
        {
            public override void Execute(Parameters p, Registers register)
                => register[p.C] = register[p.A];
        }
        public class SetI : OpCode
        {
            public override void Execute(Parameters p, Registers register)
                => register[p.C] = p.A;
        }
        public class GtIR : OpCode
        {
            public override void Execute(Parameters p, Registers register)
                => register[p.C] = p.A > register[p.B] ? 1 : 0;
        }
        public class GtRI : OpCode
        {
            public override void Execute(Parameters p, Registers register)
                => register[p.C] = register[p.A] > p.B ? 1 : 0;
        }
        public class GtRR : OpCode
        {
            public override void Execute(Parameters p, Registers register)
                => register[p.C] = register[p.A] > register[p.B] ? 1 : 0;
        }
        public class EqIR : OpCode
        {
            public override void Execute(Parameters p, Registers register)
                => register[p.C] = p.A == register[p.B] ? 1 : 0;
        }
        public class EqRI : OpCode
        {
            public override void Execute(Parameters p, Registers register)
                => register[p.C] = register[p.A] == p.B ? 1 : 0;
        }
        public class EqRR : OpCode
        {
            public override void Execute(Parameters p, Registers register)
                => register[p.C] = register[p.A] == register[p.B] ? 1 : 0;
        }
    }
}
