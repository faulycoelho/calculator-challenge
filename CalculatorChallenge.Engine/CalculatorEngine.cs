using CalculatorChallenge.Engine.Extensions;

namespace CalculatorChallenge.Engine
{
    public class CalculatorEngine
    {
        public int Execute(string? input)
        {
            var numbers = ParseInputGetNumbers(input);
            return numbers.Sum();
        }

        public int Execute(string? input, out string formula)
        {
            var numbers = ParseInputGetNumbers(input);
            var result = numbers.Sum();
            formula = $"{string.Join("+", numbers)} = {result}";
            return result;
        }

        private List<int> ParseInputGetNumbers(string? input)
        {
            var span = input.AsSpan();
            var numbers = GetNumbersFromSpan(span);
            ValidateNumbers(numbers);
            return numbers;
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
                    var customDelimiters = span.Slice(2, end - 2);
                    while (!customDelimiters.IsEmpty)
                    {
                        var customDelimiterEnd = customDelimiters.IndexOf(']');
                        var customDelimiter = customDelimiters.Slice(1, customDelimiterEnd - 1).ToString();
                        delimiters.Add(customDelimiter);
                        customDelimiters = customDelimiters.Slice(customDelimiterEnd + 1);
                    }
                }
                else
                {
                    var delimiter = span.Slice(2, end - 2);
                    if (delimiter.Length == 1)
                        delimiters.Add(delimiter.ToString());
                }

                span = span[(end + 1)..];
            }


            while (!span.IsEmpty)
            {
                int index = -1;
                int delimiterLength = 1;
                foreach (var delimiter in delimiters)
                {
                    int delimiterIndex = span.IndexOf(delimiter);
                    if (delimiterIndex > -1 && (index == -1 || delimiterIndex < index))
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
                var number = TryParseOrZero(token);
                if (number > MAX_NUMBER_ALLOWED)
                    numbers.Add(0);
                else
                    numbers.Add(number);
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
