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
            double result = Parser.ParseDouble("0.0", false);
            Assert.Equal(-1.0, result);
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
            double result = Parser.ParseDouble("-4.0", true);
            Assert.Equal(-1.0, result);
        }

        [Fact]
        public void FailOnNonNumeric()
        {
            double result = Parser.ParseDouble("a", true);
            Assert.Equal(-1.0, result);

            double result2 = Parser.ParseDouble("a.0", true);
            Assert.Equal(-1.0, result2);
        }

        [Fact]
        public void FailOnEmpty()
        {
            double result = Parser.ParseDouble("", true);
            Assert.Equal(-1.0, result);
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
            double result = Parser.ParseDoubleRange("3.0", 0.0, 2.0);
            Assert.Equal(-1.0, result);

            double result2 = Parser.ParseDoubleRange("-1.0", 0.0, 2.0);
            Assert.Equal(-1.0, result2);
        }
    }
}