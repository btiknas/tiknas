using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas.Application;
using Tiknas.AspNetCore.Components.Web;
using Tiknas.Authorization;
using Tiknas.Features;
using Tiknas.GlobalFeatures;
using Tiknas.Modularity;

namespace Tiknas.BlazoriseUI;

[DependsOn(
    typeof(TiknasAspNetCoreComponentsWebModule),
    typeof(TiknasDddApplicationContractsModule),
    typeof(TiknasAuthorizationModule),
    typeof(TiknasGlobalFeaturesModule),
    typeof(TiknasFeaturesModule)
)]
public class TiknasBlazoriseUIModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureBlazorise(context);
    }

    private void ConfigureBlazorise(ServiceConfigurationContext context)
    {
        context.Services.AddBlazorise(options =>
        {
            options.Debounce = true;
            options.DebounceInterval = 800;
        });

        context.Services.Replace(ServiceDescriptor.Scoped<IComponentActivator, ComponentActivator>());
        context.Services.AddSingleton(typeof(TiknasBlazorMessageLocalizerHelper<>));
    }
}
