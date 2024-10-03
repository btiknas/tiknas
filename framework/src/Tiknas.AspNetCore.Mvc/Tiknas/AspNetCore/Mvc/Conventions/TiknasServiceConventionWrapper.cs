using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Mvc.Conventions;

[DisableConventionalRegistration]
public class TiknasServiceConventionWrapper : IApplicationModelConvention
{
    private readonly Lazy<ITiknasServiceConvention> _convention;

    public TiknasServiceConventionWrapper(IServiceCollection services)
    {
        _convention = services.GetRequiredServiceLazy<ITiknasServiceConvention>();
    }

    public void Apply(ApplicationModel application)
    {
        _convention.Value.Apply(application);
    }
}
