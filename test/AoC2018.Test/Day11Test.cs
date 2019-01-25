using NUnit.Framework;

namespace Advent_of_Code_2018.Test
{
    public class Day11Test
    {
        [Test]
        [TestCase(3, 5, 8, 4)]
        [TestCase(122, 79, 57, -5)]
        [TestCase(217, 196, 39, 0)]
        [TestCase(101, 153, 71, 4)]
        public void Part1_getpowerlevel_should_return_correct_result(int x, int y, int serialNumber, int expectedResult)
        {
            Assert.That(Day11.GetPowerLevel((x, y), serialNumber), Is.EqualTo(expectedResult));

            Assert.That(Day11.GetHighestSquarePowerLevel(18), Is.EqualTo(29));
            Assert.That(Day11.GetHighestSquarePowerLevel(42), Is.EqualTo(30));
        }

        [Test]
        [TestCase(18, 29)]
        [TestCase(42, 30)]
        public void Part1_gethighestSquarePowerLevel_should_return_correct_result(int serialNumber, int expectedResult)
        {
            Assert.That(Day11.GetHighestSquarePowerLevel(serialNumber), Is.EqualTo(expectedResult));
        }


        [Test]
        public void Part1_real_test()
        {
            Assert.That(Day11.Part1(7689), Is.EqualTo("20,37"));
        }

        [Test]
        [Ignore("Takes ~4 minutes")]
        public void Part2_real_test()
        {
            // Takes ~4 min. :(
            Assert.That(Day11.Part2(7689), Is.EqualTo("90,169,15"));
        }
    }
}
