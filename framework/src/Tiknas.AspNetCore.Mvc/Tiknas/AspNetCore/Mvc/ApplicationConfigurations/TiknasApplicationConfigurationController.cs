using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tiknas.AspNetCore.Mvc.AntiForgery;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations;

[Area("tiknas")]
[RemoteService(Name = "tiknas")]
[Route("api/tiknas/application-configuration")]
public class TiknasApplicationConfigurationController : TiknasControllerBase, ITiknasApplicationConfigurationAppService
{
    protected readonly ITiknasApplicationConfigurationAppService ApplicationConfigurationAppService;
    protected readonly ITiknasAntiForgeryManager AntiForgeryManager;

    public TiknasApplicationConfigurationController(
        ITiknasApplicationConfigurationAppService applicationConfigurationAppService,
        ITiknasAntiForgeryManager antiForgeryManager)
    {
        ApplicationConfigurationAppService = applicationConfigurationAppService;
        AntiForgeryManager = antiForgeryManager;
    }

    [HttpGet]
    public virtual async Task<ApplicationConfigurationDto> GetAsync(
        ApplicationConfigurationRequestOptions options)
    {
        AntiForgeryManager.SetCookie();
        return await ApplicationConfigurationAppService.GetAsync(options);
    }
}
