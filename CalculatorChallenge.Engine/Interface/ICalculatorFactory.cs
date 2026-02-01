namespace CalculatorChallenge.Engine.Interface
{
    public interface ICalculatorFactory
    {
        ICalculator CreateCalculator(CalculatorChallenge.Engine.Configuration.ConfigurationOption opt);
    }
}
