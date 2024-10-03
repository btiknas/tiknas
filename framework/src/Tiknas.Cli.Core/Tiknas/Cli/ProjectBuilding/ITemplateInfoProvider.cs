using System.Threading.Tasks;
using Tiknas.Cli.ProjectBuilding.Building;

namespace Tiknas.Cli.ProjectBuilding;

public interface ITemplateInfoProvider
{
    Task<TemplateInfo> GetDefaultAsync();

    TemplateInfo Get(string name);
}
