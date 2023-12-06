using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace AdventOfCode2023;

class DayTwo : Day
{
    public override void SolvePart1(string input)
    {
        int sum = 0;

        const int redCubes = 12;
        const int greenCubes = 13;
        const int blueCubes = 14;

        string[] lines = input.Split('\n');

        foreach (var line in lines) {

            Console.WriteLine($"Current game is: {int.Parse(Regex.Replace(line.Split(":")[0], @"\D", ""))}");

            string[] sets = line.Split(":")[1].Split("; ");

            bool isValidGame = true;

            foreach (var set in sets) {
                string[] cubes = set.Trim().Split(", ");

                int totalBlueCubes = 0;
                int totalRedCubes = 0;
                int totalGreenCubes = 0;

                foreach (var cubeAmount in cubes) {
                    int amount = int.Parse(cubeAmount.Split(' ')[0]);
                    string color = cubeAmount.Split(' ')[1];

                    if (color == "blue") {
                        totalBlueCubes += amount;
                    } else if (color == "red") {
                        totalRedCubes += amount;
                    } else if (color == "green") {
                        totalGreenCubes += amount;
                    }
                }
                
                if (blueCubes < totalBlueCubes || redCubes < totalRedCubes || greenCubes < totalGreenCubes) {
                    isValidGame = false;
                    break;
                }
            }

            if (isValidGame) {
                sum += int.Parse(Regex.Replace(line.Split(":")[0], @"\D", ""));
            }
        }

        Console.WriteLine(sum);
    }

    public override void SolvePart2(string input)
    {
        throw new NotImplementedException();
    }
}