using System.Threading.Tasks;
using Tiknas.Cli.ProjectModification;

namespace Tiknas.Cli.ProjectBuilding;

public interface INugetPackageInfoProvider
{
    Task<NugetPackageInfo> GetAsync(string name);
}
