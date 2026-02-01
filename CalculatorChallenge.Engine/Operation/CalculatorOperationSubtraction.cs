using CalculatorChallenge.Engine.Configuration;

namespace CalculatorChallenge.Engine.Operation
{
    public class CalculatorOperationSubtraction : CalculatorEngineBase
    {
        public CalculatorOperationSubtraction(ConfigurationOption? customConfigurationOption = null) : base(customConfigurationOption)
        {            
        }

        protected override char OperatorSymbol => '-';

        protected override int ApplyCalc(IReadOnlyList<int> numbers)
        {
            return numbers.Skip(1).Aggregate(numbers[0], (acc, n) => acc - n);
        }
    }
}
