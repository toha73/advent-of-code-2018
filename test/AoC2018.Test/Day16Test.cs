using Advent_of_Code_2018.Test.Helpers;
using NUnit.Framework;

namespace Advent_of_Code_2018.Test
{
    public class Day16Test
    {
        private string _input;

        [SetUp]
        public void BeforeEach()
        {
            _input = ResourceHelper.GetInputStringAocDay(typeof(Day16));
        }

        [Test]
        public void Test_instructions()
        {
            Day16.Registers registers;

            registers = new Day16.Registers(3, 7, 9, 13);
            new Day16.AddR().Execute(new Day16.Parameters(0, 1, 2), registers);
            Assert.That(registers, Is.EqualTo(new Day16.Registers(3, 7, 10, 13)));

            registers = new Day16.Registers(3, 7, 9, 13);
            new Day16.AddI().Execute(new Day16.Parameters(0, 27, 2), registers);
            Assert.That(registers, Is.EqualTo(new Day16.Registers(3, 7, 30, 13)));

            registers = new Day16.Registers(3, 7, 9, 13);
            new Day16.MulR().Execute(new Day16.Parameters(0, 1, 2), registers);
            Assert.That(registers, Is.EqualTo(new Day16.Registers(3, 7, 21, 13)));

            registers = new Day16.Registers(3, 7, 9, 13);
            new Day16.MulI().Execute(new Day16.Parameters(0, 10, 2), registers);
            Assert.That(registers, Is.EqualTo(new Day16.Registers(3, 7, 30, 13)));

            registers = new Day16.Registers(3, 7, 9, 13);
            new Day16.BanR().Execute(new Day16.Parameters(0, 1, 2), registers);
            Assert.That(registers, Is.EqualTo(new Day16.Registers(3, 7, (7 & 3), 13)));

            registers = new Day16.Registers(3, 7, 9, 13);
            new Day16.BanI().Execute(new Day16.Parameters(0, 27, 2), registers);
            Assert.That(registers, Is.EqualTo(new Day16.Registers(3, 7, (27 & 3), 13)));

            registers = new Day16.Registers(3, 7, 9, 13);
            new Day16.BorR().Execute(new Day16.Parameters(0, 1, 2), registers);
            Assert.That(registers, Is.EqualTo(new Day16.Registers(3, 7, (7 | 3), 13)));

            registers = new Day16.Registers(3, 7, 9, 13);
            new Day16.BorI().Execute(new Day16.Parameters(0, 27, 2), registers);
            Assert.That(registers, Is.EqualTo(new Day16.Registers(3, 7, (27 | 3), 13)));

            registers = new Day16.Registers(3, 7, 9, 13);
            new Day16.SetR().Execute(new Day16.Parameters(0, 0, 1), registers);
            Assert.That(registers, Is.EqualTo(new Day16.Registers(3, 3, 9, 13)));

            registers = new Day16.Registers(3, 7, 9, 13);
            new Day16.SetR().Execute(new Day16.Parameters(0, 0, 2), registers);
            Assert.That(registers, Is.EqualTo(new Day16.Registers(3, 7, 3, 13)));

            registers = new Day16.Registers(3, 7, 9, 13);
            new Day16.SetR().Execute(new Day16.Parameters(0, 0, 3), registers);
            Assert.That(registers, Is.EqualTo(new Day16.Registers(3, 7, 9, 3)));
        }

        [Test]
        public void Part1_real_test()
        {
            Assert.That(Day16.Part1(_input), Is.EqualTo(547));
        }

        [Test]
        public void Part2_real_test()
        {
            Assert.That(Day16.Part2(_input), Is.EqualTo(582));
        }
    }
}
