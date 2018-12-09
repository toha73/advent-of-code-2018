using Advent_of_Code_2018.Test.Helpers;
using NUnit.Framework;

namespace Advent_of_Code_2018.Test
{
    public class Day8Test
    {
        private string _input;

        [SetUp]
        public void BeforeEach()
        {
            _input = ResourceHelper.GetInputStringAocDay(typeof(Day8));
        }

        [Test]
        public void Part1_simple_test()
        {
            Assert.That(Day8.Part1(_simpleTestInput), Is.EqualTo(138));
        }

        [Test]
        public void Part1_real_test()
        {
            Assert.That(Day8.Part1(_input), Is.EqualTo(43825));
        }

        [Test]
        public void Part2_simple_test()
        {
            Assert.That(Day8.Part2(_simpleTestInput), Is.EqualTo(66));
        }


        [Test]
        public void Part2_real_test()
        {
            Assert.That(Day8.Part2(_input), Is.EqualTo(19276));
        }

        const string _simpleTestInput = @"
2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2
";

    }
}
