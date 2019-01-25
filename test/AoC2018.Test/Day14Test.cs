using NUnit.Framework;

namespace Advent_of_Code_2018.Test
{
    public class Day14Test
    {
        [Test]
        [TestCase(9, "5158916779")]
        [TestCase(18, "9251071085")]
        [TestCase(2018, "5941429882")]
        public void Part1_simple_test1(int recipeCount, string expectedResult)
        {
            Assert.That(Day14.Part1(recipeCount), Is.EqualTo(expectedResult));
        }

        [Test]
        public void Part1_real_test()
        {
            Assert.That(Day14.Part1(607331), Is.EqualTo("8610321414"));
        }

        [Test]
        [TestCase("51589", 9)]
        [TestCase("01245", 5)]
        [TestCase("92510", 18)]
        [TestCase("59414", 2018)]
        public void Part2_simple_test1(string input, int expectedResult)
        {
            Assert.That(Day14.Part2(input), Is.EqualTo(expectedResult));
        }

        [Test]
        public void Part2_real_test()
        {
            Assert.That(Day14.Part2("607331"), Is.EqualTo(20258123));
        }
    }
}
