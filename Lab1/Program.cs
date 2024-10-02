using System;
using System.IO;

namespace Lab1
{
    public abstract class Program
    {
        private static void Main()
        {
            string basePath = FindProjectDirectory(AppContext.BaseDirectory) ??
                              throw new Exception("Could not find project directory");

            string inputFilePath = Path.Combine(basePath, "INPUT.txt");
            string outputFilePath = Path.Combine(basePath, "OUTPUT.txt");
            string[] numbers = File.ReadAllLines(inputFilePath)[0].Trim().Split(' ');
            if (numbers.Length != 2)
                throw new Exception($"Invalid numbers of inputs (2 != {numbers.Length})): {inputFilePath}");

            var ints = numbers.Select(x =>
            {
                if (int.TryParse(x, out int number))
                    return number;
                throw new Exception($"Invalid number: {x}");
            }).ToArray();

            int res = BinaryNumbersCount(ints[0], ints[1]);
            File.WriteAllText(outputFilePath, res.ToString());
        }

        private static string? FindProjectDirectory(string? currentDirectory)
        {
            while (currentDirectory != null && !Directory.GetFiles(currentDirectory, "*.csproj").Any())
            {
                currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            }

            return currentDirectory;
        }


        public static int BinaryNumbersCount(int n, int k)
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

        public static bool ZerosCount(string binary, int k)
        {
            int zeros = 0;
            foreach (var bit in binary)
                if (bit == '0')
                    zeros++;
            return zeros == k;
        }

        public static string DecimalToBinary(int number) => DecimalToBinaryHelper(number, "");

        private static string DecimalToBinaryHelper(int number, string binary)
        {
            binary = (number % 2) + binary;
            return number < 2 ? binary : DecimalToBinaryHelper(number / 2, binary);
        }
    }
}