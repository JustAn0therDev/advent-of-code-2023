using System.Text.RegularExpressions;

namespace AdventOfCode2023;

class NumberOcc {
    public double Number { get; set; }
    public int Row { get; set; }
    public int StartColumn { get; set; }
    public int EndColumn { get; set; }
}

class DayThree : Day
{
    private NumberOcc GetFullNumberWithIndex(string line, int row, int column)
    {
        // A number can only be made by having digits to the right and/or left of an already found digit.
        NumberOcc result = new() { Row = row };
 
        int columnIdx = column - 1;

        while (columnIdx >= 0 && char.IsDigit(line[columnIdx])) 
        {
            columnIdx--;
        }

        result.StartColumn = columnIdx + 1;

        columnIdx = column;

        while (columnIdx < line.Length && char.IsDigit(line[columnIdx]))
        {
            columnIdx++;
        }

        result.EndColumn = columnIdx;

        result.Number = double.Parse(string.Join("", line.Take(result.StartColumn..result.EndColumn)));

        return result;
    }

    private static bool SeenThisNumber(HashSet<NumberOcc> seenNumbers, int row, int column)
    {
        foreach (var numberOcc in seenNumbers)
        {
            if (numberOcc.Row == row && numberOcc.StartColumn <= column && numberOcc.EndColumn >= column)
            {
                return true;
            }
        }

        return false;
    }

    public override void SolvePart1(string input)
    {
        // Check for all special characters (engine parts) in the input
        // Check for adjacent digits
        // If found, use the adjacent digit to check for any digits behind and in front of it.
        // When the final number is found, save the final number, its initial and final index.
        // Sum the final number to the final answer and get to the next adjacent place
        // If the next adjacent digit has an index smaller than or equal to the final index of any of the other digits found,
        // Or has an index bigger than or equal to the initial digit of any of the other digits found,
        // Skip that digit, since it has already been seen.
        // Do that until we reach the end of the input and return the result.

        HashSet<NumberOcc> seenNumbers = new();

        string[] lines = input.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == '.') continue;

                bool isEnginePart = Regex.IsMatch(lines[i][j].ToString(), @"[^A-Za-z0-9]");

                if (isEnginePart)
                {
                    // Check for adjacent numbers
                    if (i > 0 && j > 0 && char.IsDigit(lines[i - 1][j - 1]) && !SeenThisNumber(seenNumbers, i - 1, j - 1)) 
                    {
                        seenNumbers.Add(GetFullNumberWithIndex(lines[i - 1], i - 1, j - 1));
                    }

                    if (i > 0 && char.IsDigit(lines[i - 1][j]) && !SeenThisNumber(seenNumbers, i - 1, j)) 
                    {
                        seenNumbers.Add(GetFullNumberWithIndex(lines[i - 1], i - 1, j));
                    }

                    if (i > 0 && j < lines[i].Length && char.IsDigit(lines[i - 1][j + 1]) && !SeenThisNumber(seenNumbers, i - 1, j + 1))
                    {
                        seenNumbers.Add(GetFullNumberWithIndex(lines[i - 1], i - 1, j + 1));
                    }

                    if (j > 0 && char.IsDigit(lines[i][j - 1]) && !SeenThisNumber(seenNumbers, i, j - 1)) 
                    {
                        seenNumbers.Add(GetFullNumberWithIndex(lines[i], i, j - 1));
                    }

                    if (j < lines[i].Length && char.IsDigit(lines[i][j + 1]) && !SeenThisNumber(seenNumbers, i, j + 1)) 
                    {
                        seenNumbers.Add(GetFullNumberWithIndex(lines[i], i, j + 1));
                    }

                    if (j < lines[i + 1].Length && char.IsDigit(lines[i + 1][j - 1]) && !SeenThisNumber(seenNumbers, i + 1, j - 1)) 
                    {
                        seenNumbers.Add(GetFullNumberWithIndex(lines[i + 1], i + 1, j - 1));
                    }

                    if (j < lines[i + 1].Length && char.IsDigit(lines[i + 1][j]) && !SeenThisNumber(seenNumbers, i + 1, j)) 
                    {
                        seenNumbers.Add(GetFullNumberWithIndex(lines[i + 1], i + 1, j));
                    }

                    if (j < lines[i].Length && i < lines.Length && char.IsDigit(lines[i + 1][j + 1]) && !SeenThisNumber(seenNumbers, i + 1, j + 1))
                    {
                        seenNumbers.Add(GetFullNumberWithIndex(lines[i + 1], i + 1, j + 1));
                    }
                }
            }
        }

        Console.WriteLine(seenNumbers.Sum(s => s.Number));
    }

    public override void SolvePart2(string input)
    {
        double result = 0;

        string[] lines = input.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == '.') continue;

                HashSet<NumberOcc> seenGearAdjacentNumbers = new();

                bool isGear = lines[i][j] == '*';

                if (isGear)
                {
                    if (i > 0 && j > 0 && char.IsDigit(lines[i - 1][j - 1]) && !SeenThisNumber(seenGearAdjacentNumbers, i - 1, j - 1)) 
                    {
                        seenGearAdjacentNumbers.Add(GetFullNumberWithIndex(lines[i - 1], i - 1, j - 1));
                    }

                    if (i > 0 && char.IsDigit(lines[i - 1][j]) && !SeenThisNumber(seenGearAdjacentNumbers, i - 1, j)) 
                    {
                        seenGearAdjacentNumbers.Add(GetFullNumberWithIndex(lines[i - 1], i - 1, j));
                    }

                    if (i > 0 && j < lines[i].Length && char.IsDigit(lines[i - 1][j + 1]) && !SeenThisNumber(seenGearAdjacentNumbers, i - 1, j + 1))
                    {
                        seenGearAdjacentNumbers.Add(GetFullNumberWithIndex(lines[i - 1], i - 1, j + 1));
                    }

                    if (j > 0 && char.IsDigit(lines[i][j - 1]) && !SeenThisNumber(seenGearAdjacentNumbers, i, j - 1)) 
                    {
                        seenGearAdjacentNumbers.Add(GetFullNumberWithIndex(lines[i], i, j - 1));
                    }

                    if (j < lines[i].Length && char.IsDigit(lines[i][j + 1]) && !SeenThisNumber(seenGearAdjacentNumbers, i, j + 1)) 
                    {
                        seenGearAdjacentNumbers.Add(GetFullNumberWithIndex(lines[i], i, j + 1));
                    }

                    if (j < lines[i + 1].Length && char.IsDigit(lines[i + 1][j - 1]) && !SeenThisNumber(seenGearAdjacentNumbers, i + 1, j - 1)) 
                    {
                        seenGearAdjacentNumbers.Add(GetFullNumberWithIndex(lines[i + 1], i + 1, j - 1));
                    }

                    if (j < lines[i + 1].Length && char.IsDigit(lines[i + 1][j]) && !SeenThisNumber(seenGearAdjacentNumbers, i + 1, j)) 
                    {
                        seenGearAdjacentNumbers.Add(GetFullNumberWithIndex(lines[i + 1], i + 1, j));
                    }

                    if (j < lines[i].Length && i < lines.Length && char.IsDigit(lines[i + 1][j + 1]) && !SeenThisNumber(seenGearAdjacentNumbers, i + 1, j + 1))
                    {
                        seenGearAdjacentNumbers.Add(GetFullNumberWithIndex(lines[i + 1], i + 1, j + 1));
                    }

                    if (seenGearAdjacentNumbers.Count == 2)
                    {
                        result += seenGearAdjacentNumbers.ToList()[0].Number * seenGearAdjacentNumbers.ToList()[1].Number;
                    }
                }
            }
        }

        Console.WriteLine(result);
    }
}