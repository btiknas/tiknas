using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations;

[Area("tiknas")]
[RemoteService(Name = "tiknas")]
[Route("api/tiknas/application-localization")]
public class TiknasApplicationLocalizationController: TiknasControllerBase, ITiknasApplicationLocalizationAppService
{
    private readonly ITiknasApplicationLocalizationAppService _localizationAppService;

    public TiknasApplicationLocalizationController(ITiknasApplicationLocalizationAppService localizationAppService)
    {
        _localizationAppService = localizationAppService;
    }
    
    [HttpGet]
    public virtual async Task<ApplicationLocalizationDto> GetAsync(ApplicationLocalizationRequestDto input)
    {
        return await _localizationAppService.GetAsync(input);
    }
}