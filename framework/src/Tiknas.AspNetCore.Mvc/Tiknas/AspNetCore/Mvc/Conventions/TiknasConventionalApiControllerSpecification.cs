using Asp.Versioning.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;

namespace Tiknas.AspNetCore.Mvc.Conventions;

public class TiknasConventionalApiControllerSpecification : IApiControllerSpecification
{
    private readonly TiknasAspNetCoreMvcOptions _options;

    public TiknasConventionalApiControllerSpecification(IOptions<TiknasAspNetCoreMvcOptions> options)
    {
        _options = options.Value;
    }

    public bool IsSatisfiedBy(ControllerModel controller)
    {
        var configuration = _options
            .ConventionalControllers
            .ConventionalControllerSettings
            .GetSettingOrNull(controller.ControllerType.AsType());

        return configuration != null;
    }
}
