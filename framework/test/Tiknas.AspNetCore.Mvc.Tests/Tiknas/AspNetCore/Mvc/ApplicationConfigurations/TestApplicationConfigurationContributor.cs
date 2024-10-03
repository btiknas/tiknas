using System.Threading.Tasks;
using Tiknas.Data;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations;

public class TestApplicationConfigurationContributor : IApplicationConfigurationContributor
{
    public Task ContributeAsync(ApplicationConfigurationContributorContext context)
    {
        context.ApplicationConfiguration.SetProperty("TestKey", "TestValue");
        return Task.CompletedTask;
    }
}
