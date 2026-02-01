using CalculatorChallenge.Engine.Configuration;

namespace CalculatorChallenge.Engine.Operation
{
    public class CalculatorOperationMultiplication : CalculatorEngineBase
    {
        public CalculatorOperationMultiplication(ConfigurationOption? customConfigurationOption = null) : base(customConfigurationOption)
        {            
        }

        protected override char OperatorSymbol => '*';

        protected override int ApplyCalc(IReadOnlyList<int> numbers)
        {
            return numbers.Aggregate(1, (acc, n) => acc * n); ;
        }
    }
}
