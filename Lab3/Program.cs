using Helpers;

namespace Lab3;

public static class Program
{
    private static void Main()
    {
        try
        {
            string basePath = FileSearch.FindProjectDirectory(AppContext.BaseDirectory) ??
                              throw new Exception("Could not find project directory");

            string inputFilePath = Path.Combine(basePath, "INPUT.txt");
            string outputFilePath = Path.Combine(basePath, "OUTPUT.txt");
            string[] lines = File.ReadAllLines(inputFilePath);
            Console.WriteLine($"Read lines from file [\"{inputFilePath}\"]");

            Labyrinth colorfulLabyrinth = new Labyrinth(lines);

            string result = colorfulLabyrinth.GetResult();
            File.WriteAllText(outputFilePath, result);
            Console.WriteLine($"Result is written to output file [\"{outputFilePath}\"]: {result}");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error message: " + e.Message);
        }
    }
}