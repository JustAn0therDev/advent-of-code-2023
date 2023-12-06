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
            string[] sets = line.Split(":")[1].Split("; ");

            bool isValidGame = true;

            foreach (var set in sets) {
                string[] cubes = set.Trim().Split(", ");

                int totalBlueCubes = 0;
                int totalRedCubes = 0;
                int totalGreenCubes = 0;

                foreach (var cubeAmount in cubes) {
                    string[] splitCubeInfo = cubeAmount.Split(' ');
                    int amount = int.Parse(splitCubeInfo[0]);
                    string color = splitCubeInfo[1];

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
                string gameInfo = line.Split(":")[0];
                string gameId = Regex.Replace(gameInfo, @"\D", "");
                sum += int.Parse(gameId);
            }
        }

        Console.WriteLine(sum);
    }

    public override void SolvePart2(string input)
    {
        int sum = 0;

        string[] lines = input.Split('\n');

        foreach (var line in lines) {
            string[] sets = line.Split(":")[1].Split("; ");

            int biggestBlueSet = 0;
            int biggestRedSet = 0;
            int biggestGreenSet = 0;

            foreach (var set in sets) {
                string[] cubes = set.Trim().Split(", ");

                foreach (var cubeAmount in cubes) {
                    string[] splitCubeInfo = cubeAmount.Split(' ');
                    int amount = int.Parse(splitCubeInfo[0]);
                    string color = splitCubeInfo[1];

                    if (color == "blue" && amount > biggestBlueSet) {
                        biggestBlueSet = amount;
                    } else if (color == "red" && amount > biggestRedSet) {
                        biggestRedSet = amount;
                    } else if (color == "green" && amount > biggestGreenSet) {
                        biggestGreenSet = amount;
                    }
                }
            }

            sum += biggestBlueSet * biggestRedSet * biggestGreenSet;
        }

        Console.WriteLine(sum);
    }
}