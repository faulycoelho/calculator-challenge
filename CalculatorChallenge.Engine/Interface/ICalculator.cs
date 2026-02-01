namespace CalculatorChallenge.Engine.Interface
{
    public interface ICalculator
    {
        int Execute(string? input);
        int Execute(string? input, out string formula);
    }
}
