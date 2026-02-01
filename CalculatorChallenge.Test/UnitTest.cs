using CalculatorChallenge.Engine.Operation;
using FluentAssertions;

namespace CalculatorChallenge.Test
{
    public class UnitTest
    {
        [Fact]
        public void Execute_WithSingleNumber_ShouldReturnThatNumber()
        {
            CalculatorOperationSum engine = new();
            engine.Execute("20").Should().Be(20);
        }

        [Fact]
        public void Execute_WithTwoNumbersSeparatedByComma_ShouldReturnTheirSumAndIgnoreGreaterThan1000()
        {
            CalculatorOperationSum engine = new();
            engine.Execute("1,5000").Should().Be(1);
        }
        [Fact]
        public void Execute_WithNegativeNumber_ShouldIncludeItInTheSum()
        {
            CalculatorOperationSum engine = new();
            Action act = () => engine.Execute("4,-3,-4,-9");

            act
                .Should()
                .Throw<InvalidOperationException>()
                .Which.Message
                .Should()
                .ContainAll("-3");
        }

        [Fact]
        public void Execute_WithUnlimitedNumbers_ShouldReturnTheirSum()
        {
            CalculatorOperationSum engine = new();
            engine.Execute("1,2,3,4,5,6,7,8,9,10,11,12").Should().Be(78);
        }

        [Fact]
        public void Execute_WithTwoNumbersSeparatedByCommaOrNewLine_ShouldReturnTheirSum()
        {
            CalculatorOperationSum engine = new();
            engine.Execute("1\n2,3").Should().Be(6);
        }

        [Fact]
        public void Execute_NegativeNumbers_ShouldThrowException()
        {
            CalculatorOperationSum engine = new();
            Action act = () => engine.Execute("4,-3,-4,-9");

            act
                .Should()
                .Throw<InvalidOperationException>()
                .Which.Message
                .Should()
                .ContainAll("-3", "-4", "-9");
        }


        [Fact]
        public void Execute_ShouldIgnoreValueGreatheThan1000()
        {
            CalculatorOperationSum engine = new();
            engine.Execute("2,1001,6").Should().Be(8);
        }



        [Fact]
        public void Execute_CustomDelimiter_ShouldSplitNumbers()
        {
            CalculatorOperationSum engine = new();
            engine.Execute("//#\n2#5").Should().Be(7);
            engine.Execute("//,\n2,ff,100").Should().Be(102);
        }

        [Fact]
        public void Execute_CustomDelimiterOfAnyLength_ShouldSplitNumbers()
        {
            CalculatorOperationSum engine = new();
            engine.Execute("//[***]\n11***22***33").Should().Be(66);            
        }

        [Fact]
        public void Execute_MultipleCustomDelimiterOfAnyLength_ShouldSplitNumbers()
        {
            CalculatorOperationSum engine = new();
            engine.Execute("//[*][!!][r9r]\n11r9r22*hh*33!!44").Should().Be(110);
        }


        [Fact]
        public void Execute_DisplayFormula()
        {
            CalculatorOperationSum engine = new();
            engine.Execute("2,,4,rrrr,1001,6", out string formula).Should().Be(12);
            formula.Should().Be("2+0+4+0+0+6 = 12");
        }
    }
}
