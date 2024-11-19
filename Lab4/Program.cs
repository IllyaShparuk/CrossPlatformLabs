using System.Reflection;
using McMaster.Extensions.CommandLineUtils;

namespace Lab4;

[Subcommand(typeof(Run), typeof(Version), typeof(Help), typeof(SetPath))]
public class Program
{
    public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

    private static string? ProjectVersion => Assembly.GetEntryAssembly()?.GetName().Version?.ToString();

    private static string? Authors =>
        Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;


    private void OnExecute() => CommandLineApplication.Execute<Program>("help");

    [Command(Name="help")]
    class Help
    {
        private void OnExecute()
        {
            Console.WriteLine("Commands to use:");
            Console.WriteLine("\thelp".PadRight(60) + "Show usage guide");
            Console.WriteLine("\tversion".PadRight(60) + "General information");
            Console.WriteLine("\trun <LAB> [-i|--input <file>] [-o|--output <file>]".PadRight(60) +
                              "Runs selected program");
            Console.WriteLine("\tset-path -p|--project <path>".PadRight(60) + "Sets the path as environment variable");
        }
    }

    [Command(Name = "run")]
    class Run
    {
        // TODO
        private void OnExecute() => Console.WriteLine("Hello World!");
    }

    [Command(Name = "version")]
    class Version
    {
        private void OnExecute() =>
            Console.WriteLine("\tApp version:".PadRight(20) + ProjectVersion + Environment.NewLine +
                              "\tAuthor:".PadRight(20) + Authors);
    }

    [Command(Name = "set-path")]
    class SetPath
    {
        // TODO
    }
}