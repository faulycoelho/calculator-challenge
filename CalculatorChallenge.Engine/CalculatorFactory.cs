using CalculatorChallenge.Engine.Configuration;
using CalculatorChallenge.Engine.Interface;
using static CalculatorChallenge.Engine.Enums;

namespace CalculatorChallenge.Engine
{
    public class CalculatorFactory : ICalculatorFactory
    {
        public ICalculator CreateCalculator(CalculatorOperationType calculatorOperationType, ConfigurationOption opt)
        {
            return calculatorOperationType switch
            {
                CalculatorOperationType.Sum => new Operation.CalculatorOperationSum(opt),
                CalculatorOperationType.Subtraction => new Operation.CalculatorOperationSubtraction(opt),
                CalculatorOperationType.Multiplication => new Operation.CalculatorOperationMultiplication(opt),
                CalculatorOperationType.Division => new Operation.CalculatorOperationDivision(opt),
                _ => throw new NotImplementedException()
            };            
        }
    }
}
