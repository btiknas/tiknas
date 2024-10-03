using System;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.EventBus;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Mvc.Client;

[DependsOn(
    typeof(TiknasAspNetCoreMvcClientCommonModule),
    typeof(TiknasEventBusModule)
    )]
public class TiknasAspNetCoreMvcClientModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var tiknasHostEnvironment = context.Services.GetTiknasHostEnvironment();
        if (tiknasHostEnvironment.IsDevelopment())
        {
            Configure<TiknasAspNetCoreMvcClientCacheOptions>(options =>
            {
                options.ApplicationConfigurationDtoCacheAbsoluteExpiration = TimeSpan.FromSeconds(5);
            });
        }
    }
}
