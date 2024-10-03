using System.Threading.Tasks;
using Tiknas.Application.Services;

namespace Tiknas.TestApp.Application;

[IntegrationService]
public class PeopleIntegrationService : ApplicationService, IPeopleIntegrationService
{
    public async Task<string> GetValueAsync()
    {
        return "42";
    }
}