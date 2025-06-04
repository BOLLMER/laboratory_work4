using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        int n, k;

        Console.Write("Введите количество кандидатов (n): ");
        n = int.Parse(Console.ReadLine());

        Console.Write("Введите количество избирателей (k): ");
        k = int.Parse(Console.ReadLine());

        List<List<int>> rankings = new List<List<int>>();
        List<List<int>> positions = new List<List<int>>();

        for (int i = 0; i < k; i++)
        {
            positions.Add(new List<int>(new int[n]));
        }

        for (int i = 0; i < k; ++i)
        {
            Console.Write($"\nВведите предпочтения избирателя {i + 1} (через пробел, {n} чисел от 1 до {n})\n" +"Порядок: от самого предпочтительного к наименее: ");

            List<int> ranking = Console.ReadLine()
                .Split(' ')
                .Select(num => int.Parse(num) - 1)
                .ToList();

            rankings.Add(ranking);

            for (int pos = 0; pos < n; ++pos)
            {
                int candidate = ranking[pos];
                positions[i][candidate] = pos;
            }
        }

        int[] bordaScores = new int[n];
        foreach (var ranking in rankings)
        {
            for (int pos = 0; pos < n; ++pos)
            {
                int candidate = ranking[pos];
                bordaScores[candidate] += (n - 1 - pos);
            }
        }

        int maxScore = bordaScores.Max();
        List<int> bordaWinners = new List<int>();
        for (int i = 0; i < n; ++i)
        {
            if (bordaScores[i] == maxScore)
            {
                bordaWinners.Add(i);
            }
        }

        int[,] condorcet = new int[n, n];
        for (int i = 0; i < k; ++i)
        {
            var pos = positions[i];
            for (int a = 0; a < n; ++a)
            {
                for (int b = 0; b < n; ++b)
                {
                    if (a != b && pos[a] < pos[b])
                    {
                        condorcet[a, b]++;
                    }
                }
            }
        }

        int condorcetWinner = -1;
        for (int a = 0; a < n; ++a)
        {
            bool winsAll = true;
            for (int b = 0; b < n; ++b)
            {
                if (a == b) continue;
                if (condorcet[a, b] <= k / 2)
                {
                    winsAll = false;
                    break;
                }
            }
            if (winsAll)
            {
                condorcetWinner = a;
                break;
            }
        }

        Console.Write("Победитель по Борду: ");
        foreach (int winner in bordaWinners)
        {
            Console.Write((winner + 1) + " ");
        }
        Console.WriteLine();

        if (condorcetWinner != -1)
        {
            Console.WriteLine("Победитель по Кондорсе: " + (condorcetWinner + 1));
        }
        else
        {
            Console.WriteLine("Нет победителя по Кондорсе");
        }

        bool bordaIncludesCondorcet = false;
        if (condorcetWinner != -1)
        {
            foreach (int w in bordaWinners)
            {
                if (w == condorcetWinner)
                {
                    bordaIncludesCondorcet = true;
                    break;
                }
            }
            if (!bordaIncludesCondorcet)
            {
                Console.WriteLine("Note: Different winners detected. The methods can produce different results based on voting specifics.");
            }
        }
        else if (bordaWinners.Any())
        {
            Console.WriteLine("Note: Borda method selected a winner while Condorcet method did not.");
        }
    }
}