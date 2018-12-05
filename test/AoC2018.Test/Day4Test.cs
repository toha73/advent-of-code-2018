using Advent_of_Code_2018.Test.Helpers;
using NUnit.Framework;

namespace Advent_of_Code_2018.Test
{
    public class Day4Test
    {
        private Day4 _sut;

        [SetUp]
        public void BeforeEach()
        {
            _sut = new Day4();
        }

        [Test]
        public void Part1_simple_test()
        {
            Assert.That(_sut.Part1(_simpleTestInput), Is.EqualTo(240));
        }

        [Test]
        public void Part1_real_test()
        {
            var input = ResourceHelper.GetInputString("Day4.txt");

            Assert.That(_sut.Part1(input), Is.EqualTo(60438));
        }

        [Test]
        public void Part2_simple_test()
        {
            Assert.That(_sut.Part2(_simpleTestInput), Is.EqualTo(4455));
        }


        [Test]
        public void Part2_real_test()
        {
            var input = ResourceHelper.GetInputString("Day4.txt");

            Assert.That(_sut.Part2(input), Is.EqualTo(47989));
        }

        const string _simpleTestInput = @"
[1518-11-01 00:00] Guard #10 begins shift
[1518-11-01 00:05] falls asleep
[1518-11-01 00:25] wakes up
[1518-11-01 00:30] falls asleep
[1518-11-01 00:55] wakes up
[1518-11-01 23:58] Guard #99 begins shift
[1518-11-02 00:40] falls asleep
[1518-11-02 00:50] wakes up
[1518-11-03 00:05] Guard #10 begins shift
[1518-11-03 00:24] falls asleep
[1518-11-03 00:29] wakes up
[1518-11-04 00:02] Guard #99 begins shift
[1518-11-04 00:36] falls asleep
[1518-11-04 00:46] wakes up
[1518-11-05 00:03] Guard #99 begins shift
[1518-11-05 00:45] falls asleep
[1518-11-05 00:55] wakes up
";


    }
}
