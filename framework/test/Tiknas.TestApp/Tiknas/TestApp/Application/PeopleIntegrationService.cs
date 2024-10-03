using System.Threading.Tasks;
using Tiknas.Application.Services;

namespace Tiknas.TestApp.Application;

[IntegrationService]
public class PeopleIntegrationService : ApplicationService, IPeopleIntegrationService
{
    public Task<string> GetValueAsync()
    {
        return Task.FromResult("42");
    }
}