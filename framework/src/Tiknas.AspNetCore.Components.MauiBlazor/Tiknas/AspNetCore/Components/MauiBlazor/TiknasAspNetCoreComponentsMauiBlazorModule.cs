using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.Components.Web;
using Tiknas.AspNetCore.Components.Web.Security;
using Tiknas.AspNetCore.Mvc.Client;
using Tiknas.DependencyInjection;
using Tiknas.Http.Client;
using Tiknas.Modularity;
using Tiknas.Threading;
using Tiknas.UI;

namespace Tiknas.AspNetCore.Components.MauiBlazor;

[DependsOn(
    typeof(TiknasAspNetCoreMvcClientCommonModule),
    typeof(TiknasUiModule),
    typeof(TiknasAspNetCoreComponentsWebModule)
)]
public class TiknasAspNetCoreComponentsMauiBlazorModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<TiknasHttpClientBuilderOptions>(options =>
        {
            options.ProxyClientBuildActions.Add((_, builder) =>
            {
                builder.AddHttpMessageHandler<TiknasMauiBlazorClientHttpMessageHandler>();
            });
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.ServiceProvider.GetRequiredService<IClientScopeServiceProviderAccessor>().ServiceProvider.GetRequiredService<MauiBlazorCachedApplicationConfigurationClient>().InitializeAsync();
        await context.ServiceProvider.GetRequiredService<IClientScopeServiceProviderAccessor>().ServiceProvider.GetRequiredService<TiknasComponentsClaimsCache>().InitializeAsync();
        await SetCurrentLanguageAsync(context.ServiceProvider);
    }

    private async static Task SetCurrentLanguageAsync(IServiceProvider serviceProvider)
    {
        var configurationClient = serviceProvider.GetRequiredService<ICachedApplicationConfigurationClient>();
        var utilsService = serviceProvider.GetRequiredService<ITiknasUtilsService>();
        var configuration = await configurationClient.GetAsync();
        var cultureName = configuration.Localization?.CurrentCulture?.CultureName;
        if (!cultureName.IsNullOrEmpty())
        {
            var culture = new CultureInfo(cultureName!);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        if (CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft)
        {
            await utilsService.AddClassToTagAsync("body", "rtl");
        }
    }
}