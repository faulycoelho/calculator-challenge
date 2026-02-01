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
        private const int MAX_NUMBER_ALLOWED = 1000;
       
        private static List<int> GetNumbersFromSpan(ReadOnlySpan<char> span)
        {
            List<int> numbers = new List<int>();
            var delimiters = new List<char>() { ',', '\n' };

            if (span.StartsWith("//") && span.IndexOf('\n') > 2)
            {
                var end = span.IndexOf('\n');
                var delimiter = span.Slice(2, end - 2);
                if (delimiter.Length == 1)
                    delimiters.Add(delimiter[0]);
            }


            while (!span.IsEmpty)
            {
                int index = span.IndexOfAny(delimiters.ToArray());

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
                    var number = TryParseOrZero(token);
                    if (number > MAX_NUMBER_ALLOWED)
                        numbers.Add(0);
                    else
                        numbers.Add(number);
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
