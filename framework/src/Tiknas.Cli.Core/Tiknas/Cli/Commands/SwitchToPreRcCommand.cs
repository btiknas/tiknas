using System.Text;
using System.Threading.Tasks;
using Tiknas.Cli.Args;
using Tiknas.Cli.ProjectModification;
using Tiknas.DependencyInjection;

namespace Tiknas.Cli.Commands;

public class SwitchToPreRcCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "switch-to-prerc";

    private readonly PackagePreviewSwitcher _packagePreviewSwitcher;

    public SwitchToPreRcCommand(PackagePreviewSwitcher packagePreviewSwitcher)
    {
        _packagePreviewSwitcher = packagePreviewSwitcher;
    }

    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        await _packagePreviewSwitcher.SwitchToPreRc(commandLineArgs);
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("  tiknas switch-to-prerc [options]");
        sb.AppendLine("");
        sb.AppendLine("Options:");
        sb.AppendLine("-d|--directory");
        sb.AppendLine("");
        sb.AppendLine("See the documentation for more info: https://tiknas.de/docs/latest/cli");

        return sb.ToString();
    }

    public static string GetShortDescription()
    {
        return "Switches npm packages to pre-rc TIKNAS version.";
    }
}
