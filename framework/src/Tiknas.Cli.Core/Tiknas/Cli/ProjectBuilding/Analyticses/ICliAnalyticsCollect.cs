using System.Threading.Tasks;

namespace Tiknas.Cli.ProjectBuilding.Analyticses;

public interface ICliAnalyticsCollect
{
    Task CollectAsync(CliAnalyticsCollectInputDto input);
}
