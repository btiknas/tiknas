using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.Components.DependencyInjection;
using Tiknas.DynamicProxy;
using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.ObjectMapping;
using Tiknas.Security;
using Tiknas.Timing;

namespace Tiknas.AspNetCore.Components;

[DependsOn(
    typeof(TiknasObjectMappingModule),
    typeof(TiknasSecurityModule),
    typeof(TiknasTimingModule),
    typeof(TiknasMultiTenancyAbstractionsModule)
    )]
public class TiknasAspNetCoreComponentsModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        DynamicProxyIgnoreTypes.Add<ComponentBase>();
        context.Services.AddConventionalRegistrar(new TiknasWebAssemblyConventionalRegistrar());
    }
}
