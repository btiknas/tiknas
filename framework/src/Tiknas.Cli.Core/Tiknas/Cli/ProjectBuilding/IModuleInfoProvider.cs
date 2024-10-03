using System.Collections.Generic;
using System.Threading.Tasks;
using Tiknas.Cli.ProjectBuilding.Building;

namespace Tiknas.Cli.ProjectBuilding;

public interface IModuleInfoProvider
{
    Task<ModuleInfo> GetAsync(string name);

    Task<List<ModuleInfo>> GetModuleListAsync();
}
