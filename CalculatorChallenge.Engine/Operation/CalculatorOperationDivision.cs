using CalculatorChallenge.Engine.Configuration;

namespace CalculatorChallenge.Engine.Operation
{
    public class CalculatorOperationDivision : CalculatorEngineBase
    {
        public CalculatorOperationDivision(ConfigurationOption? customConfigurationOption = null) : base(customConfigurationOption)
        {            
        }

        protected override char OperatorSymbol => '/';

        protected override int ApplyCalc(IReadOnlyList<int> numbers)
        {
            return numbers.Skip(1).Aggregate(numbers[0], (acc, n) =>
            n == 0
                ? throw new InvalidOperationException("Division by zero")
                : acc / n);
        }
    }
}
