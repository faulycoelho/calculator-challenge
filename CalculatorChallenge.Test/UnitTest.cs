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
        public void Execute_WithUnlimitedNumbers_ShouldReturnTheirSum()
        {
            Engine.CalculatorEngine engine = new();
            engine.Execute("1,2,3,4,5,6,7,8,9,10,11,12").Should().Be(78);
        }

        [Fact]
        public void Execute_WithTwoNumbersSeparatedByCommaOrNewLine_ShouldReturnTheirSum()
        {
            Engine.CalculatorEngine engine = new();
            engine.Execute(" 1\n2,3").Should().Be(6);
        }
    }
}
