using CalculatorChallenge.Engine.Extensions;

namespace CalculatorChallenge.Engine
{
    public class CalculatorEngine
    {
        public int Execute(string? input)
        {
            var span = input.AsSpan();
            var numbers = GetNumbersFromSpan(span);          
            return numbers.Sum();
        }

        private static List<int> GetNumbersFromSpan(ReadOnlySpan<char> span)
        {
            List<int> numbers = new List<int>();
            char delimiter = ',';

            while (!span.IsEmpty)
            {
                int index = span.IndexOf(delimiter);

                ReadOnlySpan<char> token;
                if (index < 0)
                {
                    token = span;
                    span = ReadOnlySpan<char>.Empty;
                }
                else
                {
                    token = span[..index];
                    span = span[(index + 1)..];
                }

                token = token.Trim();
                if (!token.IsEmpty)
                {
                    numbers.Add(TryParseOrZero(token));

                }
            }
            return numbers;
        }

        private static int TryParseOrZero(ReadOnlySpan<char> input)
        {
            int.TryParse(input, out int result);
            return result;
        }
    }
}
