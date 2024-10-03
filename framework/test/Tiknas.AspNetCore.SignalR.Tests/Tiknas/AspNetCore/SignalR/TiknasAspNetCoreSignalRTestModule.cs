using System;
using Microsoft.AspNetCore.Builder;
using Tiknas.AspNetCore.TestBase;
using Tiknas.Autofac;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.SignalR;

[DependsOn(
    typeof(TiknasAspNetCoreSignalRModule),
    typeof(TiknasAspNetCoreTestBaseModule),
    typeof(TiknasAutofacModule)
    )]
public class TiknasAspNetCoreSignalRTestModule : TiknasModule
{
    public static Exception UseConfiguredEndpointsException { get; set; }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseRouting();

        UseConfiguredEndpointsException = null;
        try
        {
            app.UseConfiguredEndpoints();
        }
        catch (Exception e)
        {
            UseConfiguredEndpointsException = e;
        }
    }
}
