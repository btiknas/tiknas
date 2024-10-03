using System.Threading.Tasks;
using Tiknas.Cli.Args;

namespace Tiknas.Cli.Commands;

public interface IConsoleCommand
{
    Task ExecuteAsync(CommandLineArgs commandLineArgs);

    string GetUsageInfo();
}
