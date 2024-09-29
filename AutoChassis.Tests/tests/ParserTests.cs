using Utilities;

namespace AutoChassis.Tests
{
    public class ParserTests
    {
        [Fact]
        public void StandardDouble()
        {
            double result = Parser.ParseDouble("1.0", true);
            Assert.Equal(1.0, result);

            double result2 = Parser.ParseDouble("333.0", true);
            Assert.Equal(333.0, result2);
        }

        [Fact]
        public void StandardInt()
        {
            double result = Parser.ParseDouble("1", false);
            Assert.Equal(1.0, result);

            double result2 = Parser.ParseDouble("333", true);
            Assert.Equal(333.0, result2);
        }

        [Fact]
        public void FailOnZeroWhenZeroNotPermitted()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Parser.ParseDouble("0.0", false));
        }

        [Fact]
        public void SucceedOnZeroWhenZeroPermitted()
        {
            double result = Parser.ParseDouble("0.0", true);
            Assert.Equal(0.0, result);
        }

        [Fact]
        public void FailOnNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Parser.ParseDouble("-4.0", false));
        }

        [Fact]
        public void FailOnNonNumeric()
        {
            Assert.Throws<FormatException>(() => Parser.ParseDouble("a", true));

            Assert.Throws<FormatException>(() => Parser.ParseDouble("a.0", true));
        }

        [Fact]
        public void FailOnEmpty()
        {
            Assert.Throws<FormatException>(() => Parser.ParseDouble("", true));
        }

        [Fact]
        public void DoubleReturnedWhenInRange()
        {
            double result = Parser.ParseDoubleRange("1.0", 0.0, 2.0);
            Assert.Equal(1.0, result);

            double result2 = Parser.ParseDoubleRange("2.0", 0.0, 2.0);
            Assert.Equal(2.0, result2);

            double result3 = Parser.ParseDoubleRange("0.0", 0.0, 2.0);
            Assert.Equal(0.0, result3);
        }

        [Fact]
        public void FailWhenOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Parser.ParseDoubleRange("3.0", 0.0, 2.0));
            Assert.Throws<ArgumentOutOfRangeException>(() => Parser.ParseDoubleRange("-1.0", 0.0, 2.0));
        }
    }
}
