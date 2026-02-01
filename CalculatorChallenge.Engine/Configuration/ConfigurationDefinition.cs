namespace CalculatorChallenge.Engine.Configuration
{
    public sealed class ConfigurationDefinition
    {
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required string DefaultValue { get; init; }
        public required Func<string, ConfigurationOption, bool> Handler { get; init; }
    }
}
