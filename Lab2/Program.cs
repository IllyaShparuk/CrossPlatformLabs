using System;
using System.IO;

namespace Lab2
{
    public abstract class Program
    {
        private static void Main()
        {
            string basePath = FindProjectDirectory(AppContext.BaseDirectory) ??
                              throw new Exception("Could not find project directory");

            string inputFilePath = Path.Combine(basePath, "INPUT.txt");
            string outputFilePath = Path.Combine(basePath, "OUTPUT.txt");
            int stepNmbr = ParseInput(inputFilePath);
            Console.WriteLine($"Parsed number from file [\"{inputFilePath}\"]: {stepNmbr}");

            long res = BallJumpingDown(stepNmbr);
            string result = res.ToString("N0");
            File.WriteAllText(outputFilePath, result);
            Console.WriteLine($"Result is written to output file [\"{outputFilePath}\"]: {result}");
        }

        public static sbyte ParseInput(string inputFilePath)
        {
            string step = File.ReadAllText(inputFilePath).Trim();
            if (!sbyte.TryParse(step, out sbyte result) || result is <= 0 or > 70)
            {
                throw new Exception($"Invalid input file [\"{inputFilePath}\"].");
            }
            return result;
        }

        private static string? FindProjectDirectory(string? currentDirectory)
        {
            while (currentDirectory != null && !Directory.GetFiles(currentDirectory, "*.csproj").Any())
            {
                currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            }

            return currentDirectory;
        }

        public static long BallJumpingDown(int n)
        {
            long[] jumpingDown = new long[n+1];
            jumpingDown[0] = 1;
            for (int i = 1; i < n + 1; i++) // Перебираємо сходи
                for (int j = Math.Max(0, i - 3); j < i; j++) // Перебираємо кількість способів
                    jumpingDown[i] += jumpingDown[j];
            return jumpingDown[n];
        }
    }
}