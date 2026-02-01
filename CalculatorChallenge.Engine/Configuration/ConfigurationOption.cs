namespace CalculatorChallenge.Engine.Configuration
{
    public sealed class ConfigurationOption
    {
        public int UpperBound { get; internal set; } = 1000;
        public bool DenyNegatives { get; internal set; } = true;
        public string AlternativeDelimiter { get; internal set; } = "\n";
    }
}
