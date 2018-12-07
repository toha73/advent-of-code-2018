using Advent_of_Code_2018.Test.Helpers;
using NUnit.Framework;

namespace Advent_of_Code_2018.Test
{
    public class Day6Test
    {
        private Day6 _sut;
        private string _input;

        [SetUp]
        public void BeforeEach()
        {
            _sut = new Day6();
            _input = ResourceHelper.GetInputStringAocDay(_sut.GetType());
        }

        [Test]
        public void Part1_simple_test()
        {
            Assert.That(_sut.Part1(_simpleTestInput), Is.EqualTo(17));
        }

        [Test]
        public void Part1_real_test()
        {
            Assert.That(_sut.Part1(_input), Is.EqualTo(2342));
        }

        [Test]
        public void Part2_simple_test()
        {
            Assert.That(_sut.Part2(_simpleTestInput, 32), Is.EqualTo(16));
        }


        [Test]
        public void Part2_real_test()
        {
            Assert.That(_sut.Part2(_input, 10000), Is.EqualTo(43302));
        }

        const string _simpleTestInput = @"
1, 1
1, 6
8, 3
3, 4
5, 5
8, 9";

    }
}
