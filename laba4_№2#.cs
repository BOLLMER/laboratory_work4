using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static double[] GenerateDoubleArray(int n)
    {
        Random random = new Random();
        double[] arr = new double[n];
        for (int i = 0; i < n; i++)
        {
            arr[i] = 100.0 + random.NextDouble() * 800.0;
        }
        return arr;
    }

    static Tuple<int, double> Task2(double[] arr, double A)
    {
        int count = 0;
        double product = 1.0;
        foreach (double num in arr)
        {
            if (Math.Abs(num) > A)
            {
                count++;
                product *= Math.Abs(num);
            }
        }
        return Tuple.Create(count, product);
    }

    static void Task3(ref double[] arr)
    {
        for (int i = 1; i < arr.Length; i += 2)
        {
            int num = (int)arr[i];
            int hundreds = (num / 100) % 10;
            int tens = (num / 10) % 10;
            if (tens > hundreds)
            {
                double temp = arr[i];
                arr[i] = arr[i - 1];
                arr[i - 1] = temp;
            }
        }
    }

    static int[] GenerateIntArray(int n)
    {
        Random random = new Random();
        int[] arr = new int[n];
        for (int i = 0; i < n; i++)
        {
            arr[i] = random.Next(10, 21);
        }
        return arr;
    }

    static int FindMostFrequent(int[] arr)
    {
        var freq = new Dictionary<int, int>();
        foreach (int num in arr)
        {
            if (freq.ContainsKey(num))
                freq[num]++;
            else
                freq[num] = 1;
        }

        int maxCount = 0, result = 0;
        foreach (var pair in freq)
        {
            if (pair.Value > maxCount)
            {
                maxCount = pair.Value;
                result = pair.Key;
            }
        }
        return result;
    }

    static int ReverseDigits(int num)
    {
        string s = Math.Abs(num).ToString();
        char[] chars = s.ToCharArray();
        Array.Sort(chars);
        Array.Reverse(chars);
        int res = int.Parse(new string(chars));
        return num < 0 ? -res : res;
    }

    static void ProcessTask5(ref int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = ReverseDigits(arr[i]);
        }
        Array.Sort(arr);
        Array.Reverse(arr);
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        int n = 10;
        double[] arr1 = GenerateDoubleArray(n);
        Console.Write("Массив 1: ");
        Console.WriteLine(string.Join(" ", arr1));
        Console.WriteLine();

        Console.Write("Введите A: ");
        double A = double.Parse(Console.ReadLine());
        var result = Task2(arr1, A);
        Console.WriteLine($"Элементов > A: {result.Item1}");
        Console.WriteLine($"Произведение после максимума: {result.Item2}");
        Console.WriteLine();

        Task3(ref arr1);
        Console.Write("После обмена: ");
        Console.WriteLine(string.Join(" ", arr1));
        Console.WriteLine();

        int m = 15;
        int[] arr4 = GenerateIntArray(m);
        Console.Write("Массив 4: ");
        Console.WriteLine(string.Join(" ", arr4));
        Console.WriteLine($"Частый элемент: {FindMostFrequent(arr4)}");
        Console.WriteLine();

        int[] arr5 = GenerateIntArray(n);
        Console.Write("Исходный массив 5: ");
        Console.WriteLine(string.Join(" ", arr5));
        int sumOrig = arr5.Sum();
        Console.WriteLine($"Сумма исходная: {sumOrig}");

        ProcessTask5(ref arr5);
        int sumNew = arr5.Sum();

        Console.Write("Обработанный массив 5: ");
        Console.WriteLine(string.Join(" ", arr5));
        Console.WriteLine($"Сумма после обработки: {sumNew}");

        if (sumOrig == sumNew)
            Console.WriteLine("Суммы равны");
        else if (sumOrig > sumNew)
            Console.WriteLine($"Сумма первого больше на {sumOrig - sumNew}");
        else
            Console.WriteLine($"Сумма второго больше на {sumNew - sumOrig}");
    }
}