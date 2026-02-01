using System.Collections.ObjectModel;

namespace CalculatorChallenge.Engine.Extensions
{
    internal static class ListExtension
    {
        internal static int SumExtension(this IReadOnlyList<int> numbers)
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
