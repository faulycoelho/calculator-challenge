while (true)
{
    Console.WriteLine("Enter your data:");
    var input = Console.ReadLine();
    var engine = new CalculatorChallenge.Engine.CalculatorEngine();
    var result = engine.Execute(input);
    Console.WriteLine($"Result: {result}");
    Console.WriteLine();
}
