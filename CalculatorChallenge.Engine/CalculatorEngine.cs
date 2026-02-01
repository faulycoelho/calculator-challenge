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
            var delimiters = new List<string>() { ",", "\n" };

            if (span.StartsWith("//"))
            {
                var end = span.IndexOf('\n');
                if (end > 2 && span[2] == '[' && span[end - 1] == ']')
                {
                    var customDelimiter = span.Slice(3, end - 4).ToString();
                    delimiters.Add(customDelimiter);
                    span = span[(end + 1)..];
                }
                else
                {
                    var delimiter = span.Slice(2, end - 2);
                    if (delimiter.Length == 1)
                        delimiters.Add(delimiter.ToString());
                }
            }


            while (!span.IsEmpty)
            {
                int index = -1;
                int delimiterLength = 1;
                foreach (var delimiter in delimiters)
                {
                    int delimiterIndex = span.IndexOf(delimiter);
                    if (delimiterIndex > 0 && (index == -1 || delimiterIndex < index))
                    {
                        index = delimiterIndex;
                        delimiterLength = delimiter.Length;
                    }
                }

                ReadOnlySpan<char> token;
                if (index < 0)
                {
                    token = span;
                    span = ReadOnlySpan<char>.Empty;
                }
                else
                {
                    token = span[..index];
                    span = span[(index + delimiterLength)..];
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
