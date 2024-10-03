using System.Threading.Tasks;

namespace Tiknas.Cli.ProjectBuilding;

public interface IProjectBuilder
{
    Task<ProjectBuildResult> BuildAsync(ProjectBuildArgs args);
}
