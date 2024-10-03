using System.Threading.Tasks;
using Tiknas.Cli.ProjectModification;

namespace Tiknas.Cli.ProjectBuilding;

public interface INpmPackageInfoProvider
{
    Task<NpmPackageInfo> GetAsync(string name);
}
