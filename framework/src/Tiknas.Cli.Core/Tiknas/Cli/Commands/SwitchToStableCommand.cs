using System.Text;
using System.Threading.Tasks;
using Tiknas.Cli.Args;
using Tiknas.Cli.ProjectModification;
using Tiknas.DependencyInjection;

namespace Tiknas.Cli.Commands;

public class SwitchToStableCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "switch-to-stable";
    
    private readonly PackagePreviewSwitcher _packagePreviewSwitcher;

    public SwitchToStableCommand(PackagePreviewSwitcher packagePreviewSwitcher)
    {
        _packagePreviewSwitcher = packagePreviewSwitcher;
    }

    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        await _packagePreviewSwitcher.SwitchToStable(commandLineArgs);
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("  tiknas switch-to-stable [options]");
        sb.AppendLine("");
        sb.AppendLine("Options:");
        sb.AppendLine("-d|--directory");
        sb.AppendLine("");
        sb.AppendLine("See the documentation for more info: https://tiknas.io/docs/latest/cli");

        return sb.ToString();
    }

    public static string GetShortDescription()
    {
        return "Switches packages to stable TIKNAS version from preview version.";
    }
}
