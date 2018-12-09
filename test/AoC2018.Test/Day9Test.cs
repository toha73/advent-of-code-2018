using NUnit.Framework;

namespace Advent_of_Code_2018.Test
{
    public class Day9Test
    {
        [Test]
        public void Part1_simple_tests()
        {
            Assert.Multiple(() =>
            {
                Assert.That(Day9.Part1And2(9, 25), Is.EqualTo(32));
                Assert.That(Day9.Part1And2(10, 1618), Is.EqualTo(8317));
                Assert.That(Day9.Part1And2(13, 7999), Is.EqualTo(146373));
                Assert.That(Day9.Part1And2(17, 1104), Is.EqualTo(2764));
                Assert.That(Day9.Part1And2(21, 6111), Is.EqualTo(54718));
                Assert.That(Day9.Part1And2(30, 5807), Is.EqualTo(37305));
            });
        }

        [Test]
        public void Part1_real_test()
        {
            Assert.That(Day9.Part1And2(423, 71944), Is.EqualTo(418237));
        }

        [Test]
        public void Part2_real_test()
        {
            Assert.That(Day9.Part1And2(423, 100 * 71944), Is.EqualTo(3505711612));
        }


    }
}
