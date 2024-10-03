using System.Threading.Tasks;
using Tiknas.Application.Services;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations;

public interface ITiknasApplicationConfigurationAppService : IApplicationService
{
    Task<ApplicationConfigurationDto> GetAsync(ApplicationConfigurationRequestOptions options);
}