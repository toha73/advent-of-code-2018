using Advent_of_Code_2018.Test.Helpers;
using NUnit.Framework;

namespace Advent_of_Code_2018.Test
{
    public class Day1Test
    {
        [Test]
        public void SimpleTest()
        {
            Assert.That(new Day1().Solution("+1, -2, +3, +1"), Is.EqualTo(3));
        }

        [Test]
        public void RealTest()
        {
            var input = ResourceHelper.GetInputString("Day1.txt");

            Assert.That(new Day1().Solution(input), Is.EqualTo(543));
        }
    }
}
