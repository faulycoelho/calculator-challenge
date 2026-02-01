using static CalculatorChallenge.Engine.Enums;

namespace CalculatorChallenge.Engine.Interface
{
    public interface ICalculatorFactory
    {
        ICalculator CreateCalculator(CalculatorOperationType calculatorOperationType, CalculatorChallenge.Engine.Configuration.ConfigurationOption opt);
    }
}
