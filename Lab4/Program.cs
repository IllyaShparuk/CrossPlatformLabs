using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Helpers;
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
    [Subcommand(typeof(Lab1Logic), typeof(Lab2Logic), typeof(Lab3Logic))]
    class Run
    {
        private void OnExecute()
        {
            Console.WriteLine("Set required Lab to execute:");
            Console.WriteLine("\tlab1, lab2 or lab3.");
            Console.WriteLine(Environment.NewLine + "Help:");
            CommandLineApplication.Execute<Program>("help");
        }

        abstract class LabBase
        {
            [Option(ShortName = "i", LongName = "input", Description = "Input file path")]
            private string InputPath { get; } = string.Empty;

            [Option(ShortName = "o", LongName = "output", Description = "Output file path")]
            private string OutputPath { get; } = string.Empty;

            protected void ExecuteLab(Action<string, string> labAction)
            {
                var input = string.IsNullOrEmpty(InputPath) ? "INPUT.txt" : InputPath;
                var output = string.IsNullOrEmpty(OutputPath) ? "OUTPUT.txt" : OutputPath;

                if (input != InputPath)
                {
                    string? labPath = Environment.GetEnvironmentVariable("LAB_PATH");
                    if (labPath != null && FileSearch.FindFile(labPath, input))
                    {
                        input = Path.Combine(labPath, input);
                    }
                    else
                    {
                        labPath = FileSearch.FindProjectDirectory(AppContext.BaseDirectory) ??
                                  throw new DirectoryNotFoundException("Could not find project directory");
                        input = Path.Combine(labPath, input);
                    }

                    if (output != OutputPath) output = Path.Combine(labPath, output);
                }
                else
                {
                    output = Path.Combine(input, "..", output);
                }

                try
                {
                    labAction(input, output);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("Error:" + e.Message);
                    throw;
                }
            }
        }

        [Command(Name = "lab1")]
        class Lab1Logic : LabBase
        {
            private void OnExecute() => ExecuteLab(ClassLibraryForLab4.Lab1.Start);
        }

        [Command(Name = "lab2")]
        class Lab2Logic : LabBase
        {
            private void OnExecute() => ExecuteLab(ClassLibraryForLab4.Lab2.Start);
        }

        [Command(Name = "lab3")]
        class Lab3Logic : LabBase
        {
            private void OnExecute() => ExecuteLab(ClassLibraryForLab4.Lab3.Start);
        }
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
        [Option(ShortName = "p", LongName = "path", Description = "The path to a file or folder"), Required]
        private string ProjectPath { get; } = string.Empty;

        private int OnExecute()
        {
            if (string.IsNullOrWhiteSpace(ProjectPath))
            {
                Console.WriteLine("Path is not specified.");
                return -1;
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
                {
                    string path = OperatingSystem.IsLinux() ? "/etc/environment" : "/etc/paths"; // /etc/paths for Mac 
                    UpdateOrAppendVariable(path, variable, value);
                }
            }
            catch (UnauthorizedAccessException e) // if sudo isn't present in dotnet run set-path || dotnet run run
            {
                string profileVariablePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    ".bash_profile");
                UpdateOrAppendVariable(profileVariablePath, variable, value, e);
                Console.WriteLine("Please run 'source ~/.bash_profile' or open a new terminal to apply changes.");
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