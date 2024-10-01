using System;
using System.IO;

namespace Lab1
{
    public abstract class Program
    {
        private static void Main()
        {
            string? basePath = AppContext.BaseDirectory;
            basePath = FindProjectDirectory(basePath);
            if (basePath == null) throw new Exception("Could not find project directory");
            string inputFilePath = Path.Combine(basePath, "INPUT.txt");
            string outputFilePath = Path.Combine(basePath, "OUTPUT.txt");
            string[] lines = File.ReadAllLines(inputFilePath);
            if (lines == null) throw new Exception("Could not read input file or there is no INPUT.txt");
            List<string> res = new List<string>();
            foreach (string line in lines)
            {
                string[] numbers = line.Split(" ");
                if (!int.TryParse(numbers[0], out int n) || !int.TryParse(numbers[1], out int k)) continue;
                res.Add(BinaryNumbersCount(n, k).ToString());
                Console.WriteLine($"n = {n}, k = {k}: {res.Last()}");
            }

            File.WriteAllLines(outputFilePath, res);
        }

        private static string? FindProjectDirectory(string? currentDirectory)
        {
            while (currentDirectory != null && !Directory.GetFiles(currentDirectory, "*.csproj").Any())
            {
                currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            }

            return currentDirectory;
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