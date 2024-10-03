using System.Threading.Tasks;
using Tiknas.Application.Services;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations;

public interface ITiknasApplicationLocalizationAppService : IApplicationService
{
    Task<ApplicationLocalizationDto> GetAsync(ApplicationLocalizationRequestDto input);
}