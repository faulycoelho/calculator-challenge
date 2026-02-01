namespace CalculatorChallenge.Engine.Extensions
{
    internal static class ListExtension
    {
        internal static int Sum(this List<int> numbers)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                sum += numbers[i];
            }
            return sum;
        }
    }
}
