using System.Text.RegularExpressions;

namespace AdventOfCode2023;

record DigitOcc(int Index, int Value);

class DayOne : Day {
    public override void SolvePart1(string input) {
        string[] inputLines = input.Split('\n');
        int sum = 0;

        foreach (var line in inputLines) {
            string onlyDigits = Regex.Replace(line, @"\D", "");
            sum += int.Parse($"{onlyDigits[0]}{onlyDigits[(onlyDigits.Length > 1 ? ^1 : 0)]}");
        }

        Console.WriteLine(sum);
    }

    private static (DigitOcc, DigitOcc) GetFirstAndLastDigitOfLine(string line) {
        List<DigitOcc> result = new();

        for (int i = 0; i < line.Length; i++) {
            if (Regex.IsMatch(line[i].ToString(), @"\d")) {
                result.Add(new DigitOcc(i, (int)char.GetNumericValue(line[i])));
            }
        }

        return result.Count > 0 
            ? (result[0], result[result.Count > 1 ? ^1 : 0]) 
            : (new DigitOcc(int.MaxValue, int.MaxValue), new DigitOcc(int.MinValue, int.MinValue));
    }

    private static (DigitOcc, DigitOcc) GetFirstAndLastWordsOfLine(string line, List<KeyValuePair<string, int>> wordToDigit) {
        List<DigitOcc> result = new();
       
        for (int i = 0; i < wordToDigit.Count; i++) {
            int index = line.IndexOf(wordToDigit[i].Key);

            while (index >= 0) {
                if (line.Contains(wordToDigit[i].Key)) {
                    result.Add(new DigitOcc(index, wordToDigit[i].Value));
                }

                index = line.IndexOf(wordToDigit[i].Key, index + 1);
            }
        }

        if (result.Count > 0) {
            result = result.OrderBy(o => o.Index).ToList();
            return (result[0], result[result.Count > 1 ? ^1 : 0]);
        }
            
        return (new DigitOcc(int.MaxValue, int.MaxValue), new DigitOcc(int.MinValue, int.MinValue));
    }

    public override void SolvePart2(string input) {
        List<KeyValuePair<string, int>> wordToDigit = new()
        {
            new("one", 1),
            new("two", 2),
            new("three", 3),
            new("four", 4),
            new("five", 5),
            new("six", 6),
            new("seven", 7),
            new("eight", 8),
            new("nine", 9)
        };

        string[] inputLines = input.Split('\n');
        
        int sum = 0;

        foreach (var line in inputLines) {
            (DigitOcc firstInt, DigitOcc lastInt) = GetFirstAndLastDigitOfLine(line);
            (DigitOcc firstWord, DigitOcc lastWord) = GetFirstAndLastWordsOfLine(line, wordToDigit);

            DigitOcc actualFirst = firstInt.Index < firstWord.Index ? firstInt : firstWord;
            DigitOcc actualLast = lastInt.Index > lastWord.Index ? lastInt : lastWord;

            sum += int.Parse(string.Concat(actualFirst.Value, actualLast.Value));
        }

        Console.WriteLine(sum);
    }
}