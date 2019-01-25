using Advent_of_Code_2018.Test.Helpers;
using NUnit.Framework;

namespace Advent_of_Code_2018.Test
{
    public class Day13Test
    {
        private string _input;

        [SetUp]
        public void BeforeEach()
        {
            _input = ResourceHelper.GetInputStringAocDay(typeof(Day13));
        }

        [Test]
        public void Part1_simple_test1()
        {
            Assert.That(Day13.Part1(_simpleInput1), Is.EqualTo("7,3"));
        }

        [Test]
        public void Part1_real_test()
        {
            Assert.That(Day13.Part1(_input), Is.EqualTo("116,10"));
        }

        [Test]
        public void Part2_simple_test1()
        {
            Assert.That(Day13.Part2(_simpleInput2), Is.EqualTo("6,4"));
        }

        [Test]
        public void Part2_real_test()
        {
            Assert.That(Day13.Part2(_input), Is.EqualTo("116,25"));
        }

        const string _simpleInput1 = @"
/->-\        
|   |  /----\
| /-+--+-\  |
| | |  | v  |
\-+-/  \-+--/
  \------/   
";

        const string _simpleInput2 = @"
/>-<\  
|   |  
| /<+-\
| | | v
\>+</ |
  |   ^
  \<->/
";

    }
}
