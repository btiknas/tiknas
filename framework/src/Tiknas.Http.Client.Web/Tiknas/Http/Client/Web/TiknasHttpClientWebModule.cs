using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas.AspNetCore.Mvc;
using Tiknas.AspNetCore.Mvc.Conventions;
using Tiknas.Http.Client.Web.Conventions;
using Tiknas.Modularity;

namespace Tiknas.Http.Client.Web;

[DependsOn(
    typeof(TiknasAspNetCoreMvcModule),
    typeof(TiknasHttpClientModule)
    )]
public class TiknasHttpClientWebModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Replace(ServiceDescriptor.Transient<ITiknasServiceConvention, TiknasHttpClientProxyServiceConvention>());
        context.Services.AddTransient<TiknasHttpClientProxyServiceConvention>();

        var partManager = context.Services.GetSingletonInstance<ApplicationPartManager>();
        partManager.FeatureProviders.Add(new TiknasHttpClientProxyControllerFeatureProvider());
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var partManager = context.ServiceProvider.GetRequiredService<ApplicationPartManager>();
        foreach (var moduleAssembly in context
            .ServiceProvider
            .GetRequiredService<IModuleContainer>()
            .Modules
            .SelectMany(m => m.AllAssemblies)
            .Where(a => a.GetTypes().Any(TiknasHttpClientProxyHelper.IsClientProxyService))
            .Distinct())
        {
            partManager.ApplicationParts.AddIfNotContains(moduleAssembly);
        }
    }
}
