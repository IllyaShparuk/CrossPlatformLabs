namespace ClassLibraryForLab4;

public static class Lab3
{
    public static void Start(string inputFilePath, string outputFilePath)
    {
        try
        {
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