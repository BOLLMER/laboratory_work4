using System;
using System.Collections.Generic;
using System.Text;

class RC4
{
    private int n;
    private List<uint> S;
    private uint i, j;
    private uint mod_mask;

    private void Ksa(List<uint> key)
    {
        uint size = 1U << n;
        uint key_len = (uint)key.Count;

        S = new List<uint>((int)size);
        for (uint k = 0; k < size; k++)
        {
            S.Add(k);
        }

        uint j = 0;
        for (uint k = 0; k < size; k++)
        {
            j = (j + S[(int)k] + key[(int)(k % key_len)]) & mod_mask;
            Swap(S, (int)k, (int)j);
        }
    }

    private void Swap(List<uint> list, int index1, int index2)
    {
        uint temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;
    }

    public RC4(int word_size, List<uint> key)
    {
        n = word_size;
        mod_mask = (1U << word_size) - 1;
        i = j = 0;
        Ksa(key);
    }

    public uint Generate()
    {
        uint size = 1U << n;

        i = (i + 1) & mod_mask;
        j = (j + S[(int)i]) & mod_mask;
        Swap(S, (int)i, (int)j);

        uint t = (S[(int)i] + S[(int)j]) & mod_mask;
        return S[(int)t];
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        string keyStr = "SecretKey";
        List<uint> key = new List<uint>();
        foreach (char c in keyStr)
        {
            key.Add((byte)c);
        }

        RC4 rc48bit = new RC4(8, key);

        Console.Write("RC4-8 (10 чисел): ");
        for (int k = 0; k < 10; k++)
        {
            Console.Write($"{rc48bit.Generate():X2} ");
        }
        Console.WriteLine("\n");

        List<uint> smallKey = new List<uint> { 0x3, 0x7, 0xB, 0xF };
        RC4 rc44bit = new RC4(4, smallKey);

        Console.Write("RC4-4 (10 чисел): ");
        for (int k = 0; k < 10; k++)
        {
            Console.Write($"{rc44bit.Generate()} ");
        }
        Console.WriteLine();
    }
}