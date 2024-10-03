using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations;

public interface IApplicationConfigurationContributor
{
    Task ContributeAsync(ApplicationConfigurationContributorContext context);
}
