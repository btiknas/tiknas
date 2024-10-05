using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Tiknas.Cli.Args;
using Tiknas.DependencyInjection;

namespace Tiknas.Cli.Commands;

public class HelpCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "help";
    
    public ILogger<HelpCommand> Logger { get; set; }
    protected TiknasCliOptions TiknasCliOptions { get; }
    protected IServiceScopeFactory ServiceScopeFactory { get; }

    public HelpCommand(IOptions<TiknasCliOptions> cliOptions,
        IServiceScopeFactory serviceScopeFactory)
    {
        ServiceScopeFactory = serviceScopeFactory;
        Logger = NullLogger<HelpCommand>.Instance;
        TiknasCliOptions = cliOptions.Value;
    }

    public Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        if (string.IsNullOrWhiteSpace(commandLineArgs.Target))
        {
            Logger.LogInformation(GetUsageInfo());
            return Task.CompletedTask;
        }

        if (!TiknasCliOptions.Commands.ContainsKey(commandLineArgs.Target))
        {
            Logger.LogWarning($"There is no command named {commandLineArgs.Target}.");
            Logger.LogInformation(GetUsageInfo());
            return Task.CompletedTask;
        }

        var commandType = TiknasCliOptions.Commands[commandLineArgs.Target];

        using (var scope = ServiceScopeFactory.CreateScope())
        {
            var command = (IConsoleCommand)scope.ServiceProvider.GetRequiredService(commandType);
            Logger.LogInformation(command.GetUsageInfo());
        }

        return Task.CompletedTask;
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("");
        sb.AppendLine("    tiknas <command> <target> [options]");
        sb.AppendLine("");
        sb.AppendLine("Command List:");
        sb.AppendLine("");

        foreach (var command in TiknasCliOptions.Commands.ToArray())
        {
            var method = command.Value.GetMethod("GetShortDescription", BindingFlags.Static | BindingFlags.Public);
            if (method == null)
            {
                continue;
            }
            
            var shortDescription = (string) method.Invoke(null, null);

            sb.Append("    > ");
            sb.Append(command.Key);
            sb.Append(string.IsNullOrWhiteSpace(shortDescription) ? "" : ":");
            sb.Append(" ");
            sb.AppendLine(shortDescription);
        }

        sb.AppendLine("");
        sb.AppendLine("To get a detailed help for a command:");
        sb.AppendLine("");
        sb.AppendLine("    tiknas help <command>");
        sb.AppendLine("");
        sb.AppendLine("See the documentation for more info: https://tiknas.de/docs/latest/cli");

        return sb.ToString();
    }

    public static string GetShortDescription()
    {
        return "Show command line help. Write ` tiknas help <command> `";
    }
}
