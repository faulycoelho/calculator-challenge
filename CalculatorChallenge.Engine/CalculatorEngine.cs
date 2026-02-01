using CalculatorChallenge.Engine.Extensions;

namespace CalculatorChallenge.Engine
{
    public class CalculatorEngine
    {
        public int Execute(string? input)
        {
            var span = input.AsSpan();
            var numbers = GetNumbersFromSpan(span);
            ValidateNumbers(numbers);
            return numbers.Sum();
        }

        private void ValidateNumbers(List<int> numbers)
        {
            List<int>? invalidNumbers = null;

            foreach (var n in numbers)
            {
                if (n < 0)
                {
                    invalidNumbers ??= new List<int>();
                    invalidNumbers.Add(n);
                }
            }

            if (invalidNumbers is not null)
            {
                throw new InvalidOperationException(
                    $"Negatives not allowed: {string.Join(", ", invalidNumbers)}");
            }
        }


        private static List<int> GetNumbersFromSpan(ReadOnlySpan<char> span)
        {
            List<int> numbers = new List<int>();
            ReadOnlySpan<char> delimiters = stackalloc char[] { ',', '\n' };

            while (!span.IsEmpty)
            {
                int index = span.IndexOfAny(delimiters);

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
