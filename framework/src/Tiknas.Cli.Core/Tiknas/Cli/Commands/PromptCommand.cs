using System.Text;
using System.Threading.Tasks;
using Tiknas.Cli.Args;
using Tiknas.DependencyInjection;

namespace Tiknas.Cli.Commands;

public class PromptCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "prompt";
    
    public Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        return Task.CompletedTask;
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("  tiknas prompt");
        sb.AppendLine("");
        sb.AppendLine("See the documentation for more info: https://tiknas.de/docs/latest/cli");

        return sb.ToString();
    }

    public static string GetShortDescription()
    {
        return "Starts with prompt mode.";
    }
}
