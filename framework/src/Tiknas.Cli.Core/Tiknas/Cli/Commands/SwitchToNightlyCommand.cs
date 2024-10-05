using System.Text;
using System.Threading.Tasks;
using Tiknas.Cli.Args;
using Tiknas.Cli.ProjectModification;
using Tiknas.DependencyInjection;

namespace Tiknas.Cli.Commands;

public class SwitchToNightlyCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "switch-to-nightly";
    
    private readonly PackagePreviewSwitcher _packagePreviewSwitcher;

    public SwitchToNightlyCommand(PackagePreviewSwitcher packagePreviewSwitcher)
    {
        _packagePreviewSwitcher = packagePreviewSwitcher;
    }

    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        await _packagePreviewSwitcher.SwitchToNightlyPreview(commandLineArgs);
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("  tiknas switch-to-nightly [options]");
        sb.AppendLine("");
        sb.AppendLine("Options:");
        sb.AppendLine("-d|--directory");
        sb.AppendLine("");
        sb.AppendLine("See the documentation for more info: https://tiknas.de/docs/latest/cli");

        return sb.ToString();
    }

    public static string GetShortDescription()
    {
        return "Switches packages to nightly preview TIKNAS version.";
    }
}
