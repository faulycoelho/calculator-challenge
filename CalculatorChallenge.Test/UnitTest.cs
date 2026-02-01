using FluentAssertions;

namespace CalculatorChallenge.Test
{
    public class UnitTest
    {
        [Fact]
        public void Execute_WithSingleNumber_ShouldReturnThatNumber()
        {
            Engine.CalculatorEngine engine = new ();
            engine.Execute("20").Should().Be(20);
        }

        [Fact]
        public void Execute_WithTwoNumbersSeparatedByComma_ShouldReturnTheirSum()
        {
            Engine.CalculatorEngine engine = new();
            engine.Execute("1,5000").Should().Be(5001);
        }
        [Fact]
        public void Execute_WithNegativeNumber_ShouldIncludeItInTheSum()
        {
            Engine.CalculatorEngine engine = new();
            engine.Execute("4,-3").Should().Be(1);
        }


        [Fact]
        public void Execute_WithMoreThanTwoNumbers_ShouldThrowException()
        {
            var engine = new Engine.CalculatorEngine();

            Assert.Throws<InvalidOperationException>(() =>
                engine.Execute("4,-3,4"));
        }
    }
}
