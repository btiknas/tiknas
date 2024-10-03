using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.MultiTenancy;
using Tiknas.AspNetCore.TestBase;
using Tiknas.Json;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;

namespace Tiknas.AspNetCore.App;

[DependsOn(
    typeof(TiknasAspNetCoreMultiTenancyModule),
    typeof(TiknasAspNetCoreTestBaseModule)
    )]
public class AppModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasMultiTenancyOptions>(options =>
        {
            options.IsEnabled = true;
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseMultiTenancy();

        app.Run(async (ctx) =>
        {
            var currentTenant = ctx.RequestServices.GetRequiredService<ICurrentTenant>();
            var jsonSerializer = ctx.RequestServices.GetRequiredService<IJsonSerializer>();

            var dictionary = new Dictionary<string, string>
            {
                ["TenantId"] = currentTenant.IsAvailable ? currentTenant.Id.ToString() : ""
            };

            var result = jsonSerializer.Serialize(dictionary, camelCase: false);
            await ctx.Response.WriteAsync(result);
        });
    }
}
