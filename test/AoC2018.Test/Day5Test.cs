using Advent_of_Code_2018.Test.Helpers;
using NUnit.Framework;

namespace Advent_of_Code_2018.Test
{
    public class Day5Test
    {
        private Day5 _sut;
        private string _input;

        [SetUp]
        public void BeforeEach()
        {
            _sut = new Day5();
            _input = ResourceHelper.GetInputStringAocDay(_sut.GetType());
        }

        [Test]
        public void Part1_simple_test()
        {
            Assert.That(_sut.Part1(_simpleTestInput), Is.EqualTo(10));
        }

        [Test]
        public void Part1_real_test()
        {
            Assert.That(_sut.Part1(_input), Is.EqualTo(10888));
        }

        [Test]
        public void Part2_simple_test()
        {
            Assert.That(_sut.Part2(_simpleTestInput), Is.EqualTo(4));
        }


        [Test]
        public void Part2_real_test()
        {
            Assert.That(_sut.Part2(_input), Is.EqualTo(6952));
        }

        const string _simpleTestInput = @"dabAcCaCBAcCcaDA";

    }
}
