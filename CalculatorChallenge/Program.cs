
if (args.Contains("--help"))
{
    ShowHelp();
    return 0;
}

var ConfiguratoinOption = new CalculatorChallenge.Engine.Configuration.ConfigurationOption();
foreach (var arg in args)
{
    var parts = arg.Split('=');

    if (parts.Length != 2)
        return ShowError($"Invalid argument format: {arg}");

    if (!CalculatorChallenge.Engine.Configuration.ConfigurationRegistry.Options.TryGetValue(parts[0], out var option))
        return ShowError($"Unknown argument: {parts[0]}");

    if (!option.Handler(parts[1], ConfiguratoinOption))
        return ShowError($"Invalid value for {parts[0]}");
}

while (true)
{
    Console.WriteLine("Enter your data:");
    var input = Console.ReadLine();
    var engine = new CalculatorChallenge.Engine.CalculatorEngine(ConfiguratoinOption);
    var result = engine.Execute(input);
    Console.WriteLine($"Result: {result}");
    Console.WriteLine();
}

int ShowError(string message)
{
    Console.WriteLine($"Error: {message}");
    Console.WriteLine();
    ShowHelp();
    return 1;
}

static void ShowHelp()
{
    Console.WriteLine("Usage:");
    Console.WriteLine("  calculator [options]");
    Console.WriteLine();
    Console.WriteLine("Options:");

    foreach (var opt in CalculatorChallenge.Engine.Configuration.ConfigurationRegistry.Options.Values)
    {
        Console.WriteLine(
            $"{opt.Name} {opt.Description} (default: {opt.DefaultValue})");
    }

    Console.WriteLine("--help Show this help");
    Console.WriteLine();
    Console.WriteLine("Examples:");
    Console.WriteLine("  calculator --upper-bound=500");
    Console.WriteLine("  calculator --alt-delimiter=x");
    Console.WriteLine("  calculator --deny-negatives=false");
}