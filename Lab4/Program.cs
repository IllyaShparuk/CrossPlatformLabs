using System.ComponentModel.DataAnnotations;
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

    [Command(Name = "help")]
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
        [Option(ShortName = "i", LongName = "input", Description = "The path to a file or folder"), Required]
        private string ProjectPath { get; } = null!;

        private int OnExecute()
        {
            if (string.IsNullOrWhiteSpace(ProjectPath))
            {
                Console.WriteLine("Path is not specified.");
                return 1;
            }

            SetEnvironmentVariable("LAB_PATH", ProjectPath);
            Console.WriteLine($"Variable LAB_PATH set to: {ProjectPath}");
            return 0;
        }

        private void SetEnvironmentVariable(string variable, string value)
        {
            try
            {
                if (OperatingSystem.IsWindows())
                    Environment.SetEnvironmentVariable(variable, value, EnvironmentVariableTarget.Machine);
                else
                    UpdateOrAppendVariable("/etc/environment", variable, value);
            }
            catch (UnauthorizedAccessException e)
            {
                string profileVariablePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    ".bashrc");
                UpdateOrAppendVariable(profileVariablePath, variable, value, e);
                Console.WriteLine("Please run 'source ~/.bashrc' or open a new terminal to apply changes.");
            }
        }

        private void UpdateOrAppendVariable(string filePath, string variable, string value,
            UnauthorizedAccessException? exception = null)
        {
            var lines = File.ReadAllLines(filePath).ToList();
            bool updated = false;

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith($"{variable}=") || lines[i].StartsWith($"export {variable}="))
                {
                    lines[i] = CheckException(exception) + $"{variable}={value}";
                    updated = true;
                    break;
                }
            }

            if (!updated)
                lines.Add(CheckException(exception) + $"{variable}={value}");

            File.WriteAllLines(filePath, lines);
        }

        string CheckException(UnauthorizedAccessException? e = null) => e != null ? "export " : "";
    }
}