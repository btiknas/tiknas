using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas.AspNetCore.Components.DependencyInjection;
using Tiknas.AspNetCore.Components.Web;
using Tiknas.Modularity;
using Tiknas.UI;

namespace Tiknas.AspNetCore.Components.Web;

[DependsOn(
    typeof(TiknasUiModule),
    typeof(TiknasAspNetCoreComponentsModule)
    )]
public class TiknasAspNetCoreComponentsWebModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Replace(ServiceDescriptor.Transient<IComponentActivator, ServiceProviderComponentActivator>());

        var preActions = context.Services.GetPreConfigureActions<TiknasAspNetCoreComponentsWebOptions>();
        Configure<TiknasAspNetCoreComponentsWebOptions>(options =>
        {
            preActions.Configure(options);
        });
    }
}
