using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tiknas.AspNetCore.Controllers;
using Tiknas.AspNetCore.Mvc.ApplicationConfigurations;

namespace Tiknas.AspNetCore.Mvc.Controllers;

[Area("tiknas")]
[RemoteService(Name = "tiknas")]
[ReplaceControllers(typeof(TiknasApplicationConfigurationController), typeof(TiknasApplicationLocalizationController))]
public class ReplaceBuiltInController : TiknasController
{
    [HttpGet("api/tiknas/application-configuration")]
    public virtual Task<MyApplicationConfigurationDto> GetAsync(MyApplicationConfigurationRequestOptions options)
    {
        return Task.FromResult(new MyApplicationConfigurationDto()
        {
            Random = options.Random
        });
    }

    [HttpGet("api/tiknas/application-localization")]
    public virtual Task<MyApplicationLocalizationDto> GetAsync(MyApplicationLocalizationRequestDto input)
    {
        return Task.FromResult(new MyApplicationLocalizationDto()
        {
            Random = input.Random
        });
    }
}

public class MyApplicationConfigurationRequestOptions : ApplicationConfigurationRequestOptions
{
    public string Random { get; set; }
}

public class MyApplicationConfigurationDto : ApplicationConfigurationDto
{
    public string Random { get; set; }
}

public class MyApplicationLocalizationRequestDto : ApplicationLocalizationRequestDto
{
    public string Random { get; set; }
}

public class MyApplicationLocalizationDto : ApplicationLocalizationDto
{
    public string Random { get; set; }
}
