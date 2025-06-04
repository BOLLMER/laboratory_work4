using System;
using System.Collections.Generic;

public struct GameResult
{
    public int Score1;
    public int Score2;
}

public class Program
{
    // Стратегии
    public static bool AlwaysCooperate(int roundNumber, List<bool> selfChoices, List<bool> enemyChoices)
    {
        return true;
    }

    public static bool AlwaysBetray(int roundNumber, List<bool> selfChoices, List<bool> enemyChoices)
    {
        return false;
    }

    public static bool TitForTat(int roundNumber, List<bool> selfChoices, List<bool> enemyChoices)
    {
        if (enemyChoices.Count == 0)
        {
            return true;
        }
        return enemyChoices[enemyChoices.Count - 1];
    }

    public static GameResult PlayGame(
        Func<int, List<bool>, List<bool>, bool> algo1,
        Func<int, List<bool>, List<bool>, bool> algo2)
    {
        Random random = new Random();
        int rounds = random.Next(100, 201);

        List<bool> choices1 = new List<bool>();
        List<bool> choices2 = new List<bool>();
        int score1 = 0;
        int score2 = 0;

        for (int round = 1; round <= rounds; round++)
        {
            bool choice1 = algo1(round, choices1, choices2);
            bool choice2 = algo2(round, choices2, choices1);

            if (choice1 && choice2)
            {
                score1 += 24;
                score2 += 24;
            }
            else if (!choice1 && !choice2)
            {
                score1 += 4;
                score2 += 4;
            }
            else if (choice1 && !choice2)
            {
                score2 += 20;
            }
            else
            {
                score1 += 20;
            }

            choices1.Add(choice1);
            choices2.Add(choice2);
        }

        return new GameResult { Score1 = score1, Score2 = score2 };
    }

    public static void Main()
    {
        GameResult result = PlayGame(TitForTat, AlwaysBetray);
        Console.WriteLine($"Счет алгоритма 1 (Tit-for-Tat): {result.Score1}");
        Console.WriteLine($"Счет алгоритма 2 (Всегда предавать): {result.Score2}");
    }
}