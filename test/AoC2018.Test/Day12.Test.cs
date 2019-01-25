using Advent_of_Code_2018.Test.Helpers;
using NUnit.Framework;

namespace Advent_of_Code_2018.Test
{
    public class Day12Test
    {
        private string _input;

        [SetUp]
        public void BeforeEach()
        {
            _input = ResourceHelper.GetInputStringAocDay(typeof(Day12));
        }


        [Test]
        public void Part1_simple_test()
        {
            Assert.That(Day12.Part1(_simpleInput, 20), Is.EqualTo(325));
        }

        [Test]
        public void Part1_real_test()
        {
            Assert.That(Day12.Part1(_input, 20), Is.EqualTo(2063));
        }

        [Test]
        public void Part2_real_test()
        {
            Assert.That(Day12.Part2(_input, 50000000000), Is.EqualTo(1600000000328));
        }

        const string _simpleInput = @"initial state: #..#.#..##......###...###

...## => #
..#.. => #
.#... => #
.#.#. => #
.#.## => #
.##.. => #
.#### => #
#.#.# => #
#.### => #
##.#. => #
##.## => #
###.. => #
###.# => #
####. => #
";

    }
}
