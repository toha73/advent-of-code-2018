using Advent_of_Code_2018.Test.Helpers;
using NUnit.Framework;

namespace Advent_of_Code_2018.Test
{
    public class Day7Test
    {
        private string _input;

        [SetUp]
        public void BeforeEach()
        {
            _input = ResourceHelper.GetInputStringAocDay(typeof(Day7));
        }

        [Test]
        public void Part1_simple_test()
        {
            Assert.That(Day7.Part1(_simpleTestInput), Is.EqualTo("CABDFE"));
        }

        [Test]
        public void Part1_real_test()
        {
            Assert.That(Day7.Part1(_input), Is.EqualTo("GLMVWXZDKOUCEJRHFAPITSBQNY"));
        }

        [Test]
        public void Part2_simple_test()
        {
            Assert.That(Day7.Part2(_simpleTestInput, 2, 0), Is.EqualTo(15));
        }


        [Test]
        public void Part2_real_test()
        {
            Assert.That(Day7.Part2(_input, 5, 60), Is.EqualTo(1105));
        }

        const string _simpleTestInput = @"
Step C must be finished before step A can begin.
Step C must be finished before step F can begin.
Step A must be finished before step B can begin.
Step A must be finished before step D can begin.
Step B must be finished before step E can begin.
Step D must be finished before step E can begin.
Step F must be finished before step E can begin.";

    }
}
