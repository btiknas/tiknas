using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.Mvc.Client;
using Tiknas.DependencyInjection;
using Tiknas.Modularity;

namespace Tiknas.Maui.Client;

[DependsOn(
    typeof(TiknasAspNetCoreMvcClientCommonModule)
)]
public class TiknasMauiClientModule : TiknasModule
{
    public async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.ServiceProvider.GetRequiredService<IClientScopeServiceProviderAccessor>().ServiceProvider.GetRequiredService<MauiCachedApplicationConfigurationClient>().InitializeAsync();
    }
}
