using Advent_of_Code_2018.Test.Helpers;
using NUnit.Framework;

namespace Advent_of_Code_2018.Test
{
    public class Day2Test
    {
        private Day2 _sut;

        [SetUp]
        public void BeforeEach()
        {
            _sut = new Day2();
        }

        [Test]
        public void Part1_simple_test()
        {
            Assert.That(_sut.Part1(_simpleTestInputPart1), Is.EqualTo(12));
        }

        [Test]
        public void Part1_real_test()
        {
            var input = ResourceHelper.GetInputString("Day2.txt");

            Assert.That(_sut.Part1(input), Is.EqualTo(6200));
        }

        [Test]
        public void Part2_simple_test()
        {
            Assert.That(_sut.Part2(_simpleTestInputPart2), Is.EqualTo("fgij"));
        }


        [Test]
        public void Part2_real_test()
        {
            var input = ResourceHelper.GetInputString("Day2.txt");

            Assert.That(_sut.Part2(input), Is.EqualTo("xpysnnkqrbuhefmcajodplyzw"));
        }

        const string _simpleTestInputPart1 = @"
abcdef
bababc
abbcde
abcccd
aabcdd
abcdee
ababab
";

        const string _simpleTestInputPart2 = @"
abcde
fghij
klmno
pqrst
fguij
axcye
wvxyz
";

    }
}
