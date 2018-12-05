using Advent_of_Code_2018.Test.Helpers;
using NUnit.Framework;

namespace Advent_of_Code_2018.Test
{
    public class Day1Test
    {
        private Day1 _sut;

        [SetUp]
        public void BeforeEach()
        {
            _sut = new Day1();
        }

        [Test]
        public void Part1_simple_test()
        {
            Assert.That(new Day1().Part1(_simpleTestInput), Is.EqualTo(3));
        }

        [Test]
        public void Part1_real_test()
        {
            var input = ResourceHelper.GetInputString("Day1.txt");

            Assert.That(new Day1().Part1(input), Is.EqualTo(543));
        }

        [Test]
        public void Part2_simple_test()
        {
            Assert.That(new Day1().Part2(_simpleTestInput), Is.EqualTo(2));
        }


        [Test]
        public void Part2_real_test()
        {
            var input = ResourceHelper.GetInputString("Day1.txt");

            Assert.That(new Day1().Part2(input), Is.EqualTo(621));
        }

        const string _simpleTestInput = "+1,-2,+3,+1";

    }
}
