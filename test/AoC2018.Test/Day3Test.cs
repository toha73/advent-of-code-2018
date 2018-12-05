using Advent_of_Code_2018.Test.Helpers;
using NUnit.Framework;

namespace Advent_of_Code_2018.Test
{
    public class Day3Test
    {
        private Day3 _sut;

        [SetUp]
        public void BeforeEach()
        {
            _sut = new Day3();
        }

        [Test]
        public void Part1_simple_test()
        {
            Assert.That(_sut.Part1(_simpleTestInput), Is.EqualTo(4));
        }

        [Test]
        public void Part1_real_test()
        {
            var input = ResourceHelper.GetInputString("Day3.txt");

            Assert.That(_sut.Part1(input), Is.EqualTo(118539));
        }

        [Test]
        public void Part2_simple_test()
        {
            Assert.That(_sut.Part2(_simpleTestInput), Is.EqualTo(3));
        }


        [Test]
        public void Part2_real_test()
        {
            var input = ResourceHelper.GetInputString("Day3.txt");

            Assert.That(_sut.Part2(input), Is.EqualTo(1270));
        }

        const string _simpleTestInput = @"
#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2
";


    }
}
