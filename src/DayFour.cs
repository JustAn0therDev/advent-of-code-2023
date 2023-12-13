namespace AdventOfCode2023;

class DayFour : Day
{
    public override void SolvePart1(string input)
    {
        int sum = 0;
        string[] lines = input.Split('\n');

        foreach (var line in lines)
        {
            string[] numbers = line.Split(':')[1].Split('|');

            List<int> winningNumbers = 
                numbers[0].Trim().Split(' ').Where(w => !string.IsNullOrEmpty(w)).Select(s => int.Parse(s.Trim())).ToList();
            List<int> currentNumbers = 
                numbers[1].Trim().Split(' ').Where(w => !string.IsNullOrEmpty(w)).Select(s => int.Parse(s.Trim())).ToList();

            int points = currentNumbers.Intersect(winningNumbers).Count();

            int finalScore = 0;

            while (points > 0)
            {
                if (finalScore == 0) 
                {
                    finalScore = 1;
                } 
                else 
                {
                    finalScore *= 2;
                }
                
                points--;
            }

            sum += finalScore;
        }

        Console.WriteLine(sum);
    }

    public override void SolvePart2(string input)
    {
        throw new NotImplementedException();
    }
}