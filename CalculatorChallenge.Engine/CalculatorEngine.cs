using CalculatorChallenge.Engine.Configuration;
using CalculatorChallenge.Engine.Extensions;

namespace CalculatorChallenge.Engine
{
    public class CalculatorEngine
    {
        private readonly ConfigurationOption configurationOption;
        public CalculatorEngine(ConfigurationOption? customConfigurationOption = null)
        {
            configurationOption = customConfigurationOption ?? new ConfigurationOption();
        }

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
            if (configurationOption.DenyNegatives)
            {
                ValidateNegativeNumbers(numbers);
            }

            return numbers;
        }

        private void ValidateNegativeNumbers(List<int> numbers)
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

        private List<int> GetNumbersFromSpan(ReadOnlySpan<char> span)
        {
            List<int> numbers = new List<int>();
            var delimiters = new List<string>() { ",", configurationOption.AlternativeDelimiter };

            if (span.StartsWith("//"))
            {
                int end = TryFindNewLineEscape(span, out bool byDoubleSlash);
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

                span = span[(end + (byDoubleSlash ? 2 : 1))..];
            }


            while (!span.IsEmpty)
            {
                int index = -1;
                int delimiterLength = 1;
                foreach (var delimiter in delimiters)
                {
                    bool byDoubleSlash = false;
                    int delimiterIndex = delimiter == "\n".AsSpan()
                        ? TryFindNewLineEscape(span, out byDoubleSlash)
                        : span.IndexOf(delimiter);

                    if (delimiterIndex > -1 && (index == -1 || delimiterIndex < index))
                    {
                        index = delimiterIndex;
                        delimiterLength = delimiter.Length + (byDoubleSlash ? 1 : 0);
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
                if (number > configurationOption.UpperBound)
                    numbers.Add(0);
                else
                    numbers.Add(number);
            }
            return numbers;
        }

        private static int TryFindNewLineEscape(ReadOnlySpan<char> span, out bool ByDoubleSlash)
        {
            ByDoubleSlash = false;
            var index = span.IndexOf('\n');
            if (index == -1)
            {
                int backslashIndex = span.IndexOf('\\');
                if (backslashIndex != -1 && backslashIndex + 1 < span.Length && span[backslashIndex + 1] == 'n')
                {
                    index = backslashIndex;
                    ByDoubleSlash = true;
                }
            }

            return index;
        }

        private static int TryParseOrZero(ReadOnlySpan<char> input)
        {
            int.TryParse(input, out int result);
            return result;
        }
    }
}
