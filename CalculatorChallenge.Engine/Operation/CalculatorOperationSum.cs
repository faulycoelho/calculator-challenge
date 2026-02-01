using CalculatorChallenge.Engine.Configuration;
using CalculatorChallenge.Engine.Extensions;

namespace CalculatorChallenge.Engine.Operation
{
    public class CalculatorOperationSum : CalculatorEngineBase
    {
        public CalculatorOperationSum(ConfigurationOption? customConfigurationOption = null) : base(customConfigurationOption)
        {            
        }

        protected override char OperatorSymbol => '+';
        protected override int ApplyCalc(IReadOnlyList<int> numbers)
        {
            return numbers.SumExtension();
        }
    }
}
