using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Tiknas.AspNetCore.Mvc.Conventions;

public class TiknasConventionalControllerFeatureProvider : ControllerFeatureProvider
{
    private readonly ITiknasApplication _application;

    public TiknasConventionalControllerFeatureProvider(ITiknasApplication application)
    {
        _application = application;
    }

    protected override bool IsController(TypeInfo typeInfo)
    {
        //TODO: Move this to a lazy loaded field for efficiency.
        if (_application.ServiceProvider == null)
        {
            return false;
        }

        var configuration = _application.ServiceProvider
            .GetRequiredService<IOptions<TiknasAspNetCoreMvcOptions>>().Value
            .ConventionalControllers
            .ConventionalControllerSettings
            .GetSettingOrNull(typeInfo.AsType());

        return configuration != null;
    }
}
