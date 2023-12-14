using System.Text.RegularExpressions;

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
        string[] lines = input.Split('\n');

        Dictionary<int, int> cardToAmount = new();

        foreach (var line in lines)
        {
            int card = int.Parse(Regex.Replace(line.Split(':')[0], @"\D", ""));

            if (cardToAmount.ContainsKey(card)) 
            {
                cardToAmount[card]++;
            } 
            else 
            {
                cardToAmount.Add(card, 1);
            }

            string[] numbers = line.Split(':')[1].Split('|');

            List<int> winningNumbers = 
                numbers[0].Trim().Split(' ').Where(w => !string.IsNullOrEmpty(w)).Select(s => int.Parse(s.Trim())).ToList();
            List<int> currentNumbers = 
                numbers[1].Trim().Split(' ').Where(w => !string.IsNullOrEmpty(w)).Select(s => int.Parse(s.Trim())).ToList();

            int points = currentNumbers.Intersect(winningNumbers).Count();

            for (int i = 1; i <= cardToAmount[card]; i++)
            {
                for (int j = card + 1; j <= card + points; j++)
                {
                    if (cardToAmount.ContainsKey(j)) 
                    {
                        cardToAmount[j]++;
                    } 
                    else 
                    {
                        cardToAmount.Add(j, 1);
                    }
                }
            }
        }

        Console.WriteLine(cardToAmount.Values.Sum());
    }
}