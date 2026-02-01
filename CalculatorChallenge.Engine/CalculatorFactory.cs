using CalculatorChallenge.Engine.Configuration;
using CalculatorChallenge.Engine.Interface;

namespace CalculatorChallenge.Engine
{
    public class CalculatorFactory : ICalculatorFactory
    {
        public ICalculator CreateCalculator(ConfigurationOption opt)
        {
            return new CalculatorEngine(opt);
        }
    }
}
