using System;
using System.IO;

namespace Lab1
{
    public abstract class Program
    {
        private static void Main()
        {
            Console.WriteLine(BinaryNumbersCount(121, 2));
        }

        private static int BinaryNumbersCount(int n, int k)
        {
            int count = 0;
            for (int i = 1; i <= n; i++)
            {
                string binary = DecimalToBinary(i);
                if (ZerosCount(binary, k))
                    count++;
            }
            return count;
        }

        private static bool ZerosCount(string binary, int k)
        {
            int zeros = 0;
            foreach (var bit in binary)
                if (bit == '0')
                    zeros++;
            return zeros == k;
        }

        private static string DecimalToBinary(int number) => DecimalToBinaryHelper(number, "");

        private static string DecimalToBinaryHelper(int number, string binary)
        {
            binary = (number % 2) + binary;
            return number < 2 ? binary : DecimalToBinaryHelper(number / 2, binary);
        }
    }
}