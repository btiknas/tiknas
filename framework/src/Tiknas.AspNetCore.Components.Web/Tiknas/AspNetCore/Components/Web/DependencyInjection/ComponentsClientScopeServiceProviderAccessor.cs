using System;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.Web.DependencyInjection;

public class ComponentsClientScopeServiceProviderAccessor :
    IClientScopeServiceProviderAccessor,
    ISingletonDependency
{
    public IServiceProvider ServiceProvider { get; set; } = default!;
}
