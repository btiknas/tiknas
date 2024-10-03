using Microsoft.AspNetCore.Builder;
using Tiknas.AspNetCore.MultiTenancy;
using Tiknas.AspNetCore.Mvc;
using Tiknas.AspNetCore.Serilog;
using Tiknas.AspNetCore.TestBase;
using Tiknas.Autofac;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;

namespace Tiknas.AspNetCore.App;

[DependsOn(
    typeof(TiknasAspNetCoreTestBaseModule),
    typeof(TiknasAspNetCoreMvcModule),
    typeof(TiknasAspNetCoreMultiTenancyModule),
    typeof(TiknasAspNetCoreSerilogModule),
    typeof(TiknasAutofacModule)
)]
public class TiknasSerilogTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasMultiTenancyOptions>(options => { options.IsEnabled = true; });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseCorrelationId();
        app.UseRouting();
        app.UseAuthorization();
        app.UseMultiTenancy();
        app.UseAuditing();
        app.UseTiknasSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
