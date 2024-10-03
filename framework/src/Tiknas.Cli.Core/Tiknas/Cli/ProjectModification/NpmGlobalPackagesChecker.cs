using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Tiknas.Cli.Utils;
using Tiknas.DependencyInjection;

namespace Tiknas.Cli.ProjectModification;

public class NpmGlobalPackagesChecker : ITransientDependency
{
    public NpmHelper NpmHelper { get; }
    public ILogger<NpmGlobalPackagesChecker> Logger { get; set; }

    public NpmGlobalPackagesChecker(NpmHelper npmHelper)
    {
        NpmHelper = npmHelper;
        Logger = NullLogger<NpmGlobalPackagesChecker>.Instance;
    }

    public void Check()
    {
        var installedNpmPackages = NpmHelper.GetInstalledNpmPackages();

        if (!installedNpmPackages.Contains(" yarn@"))
        {
            NpmHelper.InstallYarn();
        }
    }
}
