using Advent_of_Code_2018.Test.Helpers;
using NUnit.Framework;

namespace Advent_of_Code_2018.Test
{
    public class Day10Test
    {
        private string _input;

        [SetUp]
        public void BeforeEach()
        {
            _input = ResourceHelper.GetInputStringAocDay(typeof(Day10));
        }


        [Test]
        public void Part1_simple_test()
        {
            var result = Day10.Part1(_simpleInput);

            Assert.Pass("Bitmap with text: HI");
        }


        [Test]
        public void Part1_real_test()
        {
            var result = Day10.Part1(_input);

            Assert.Pass("Bitmap with text: XECXBPZB");
        }


        [Test]
        public void Part2_simple_test()
        {
            Assert.That(Day10.Part2(_simpleInput), Is.EqualTo(3));
        }


        [Test]
        public void Part2_real_test()
        {
            Assert.That(Day10.Part2(_input), Is.EqualTo(10124));
        }

        private const string _simpleInput = @"
position=< 9,  1> velocity=< 0,  2>
position=< 7,  0> velocity=<-1,  0>
position=< 3, -2> velocity=<-1,  1>
position=< 6, 10> velocity=<-2, -1>
position=< 2, -4> velocity=< 2,  2>
position=<-6, 10> velocity=< 2, -2>
position=< 1,  8> velocity=< 1, -1>
position=< 1,  7> velocity=< 1,  0>
position=<-3, 11> velocity=< 1, -2>
position=< 7,  6> velocity=<-1, -1>
position=<-2,  3> velocity=< 1,  0>
position=<-4,  3> velocity=< 2,  0>
position=<10, -3> velocity=<-1,  1>
position=< 5, 11> velocity=< 1, -2>
position=< 4,  7> velocity=< 0, -1>
position=< 8, -2> velocity=< 0,  1>
position=<15,  0> velocity=<-2,  0>
position=< 1,  6> velocity=< 1,  0>
position=< 8,  9> velocity=< 0, -1>
position=< 3,  3> velocity=<-1,  1>
position=< 0,  5> velocity=< 0, -1>
position=<-2,  2> velocity=< 2,  0>
position=< 5, -2> velocity=< 1,  2>
position=< 1,  4> velocity=< 2,  1>
position=<-2,  7> velocity=< 2, -2>
position=< 3,  6> velocity=<-1, -1>
position=< 5,  0> velocity=< 1,  0>
position=<-6,  0> velocity=< 2,  0>
position=< 5,  9> velocity=< 1, -2>
position=<14,  7> velocity=<-2,  0>
position=<-3,  6> velocity=< 2, -1>
";
    }
}
