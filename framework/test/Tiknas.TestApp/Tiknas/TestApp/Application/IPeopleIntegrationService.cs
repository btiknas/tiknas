using System.Threading.Tasks;
using Tiknas.Application.Services;

namespace Tiknas.TestApp.Application;

public interface IPeopleIntegrationService : IApplicationService
{
    Task<string> GetValueAsync();
}