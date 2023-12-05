using System.Text.RegularExpressions;

namespace AdventOfCode2023;

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

    public override void SolvePart2(string input) {

    }
}