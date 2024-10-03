using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Tiknas.AspNetCore.Components.Web;
using Tiknas.AspNetCore.Components.Web.ExceptionHandling;
using Tiknas.AspNetCore.Components.Web.Security;
using Tiknas.AspNetCore.Mvc.Client;
using Tiknas.DependencyInjection;
using Tiknas.Http.Client;
using Tiknas.Modularity;
using Tiknas.Threading;
using Tiknas.UI;

namespace Tiknas.AspNetCore.Components.WebAssembly;

[DependsOn(
    typeof(TiknasAspNetCoreMvcClientCommonModule),
    typeof(TiknasUiModule),
    typeof(TiknasAspNetCoreComponentsWebModule)
)]
public class TiknasAspNetCoreComponentsWebAssemblyModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var tiknasHostEnvironment = context.Services.GetSingletonInstance<ITiknasHostEnvironment>();
        if (tiknasHostEnvironment.EnvironmentName.IsNullOrWhiteSpace())
        {
            tiknasHostEnvironment.EnvironmentName = context.Services.GetWebAssemblyHostEnvironment().Environment;
        }

        PreConfigure<TiknasHttpClientBuilderOptions>(options =>
        {
            options.ProxyClientBuildActions.Add((_, builder) =>
            {
                builder.AddHttpMessageHandler<TiknasBlazorClientHttpMessageHandler>();
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClient();
        context.Services
            .GetHostBuilder().Logging
            .AddProvider(new TiknasExceptionHandlingLoggerProvider(context.Services));
        
        if (!context.Services.ExecutePreConfiguredActions<TiknasAspNetCoreComponentsWebOptions>().IsBlazorWebApp)
        {
            Configure<TiknasAuthenticationOptions>(options =>
            {
                options.LoginUrl = "authentication/login";
                options.LogoutUrl = "authentication/logout";
            });
        }
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        var msAuthenticationStateProvider = context.Services.FirstOrDefault(x => x.ServiceType == typeof(AuthenticationStateProvider));
        if (msAuthenticationStateProvider is {ImplementationType: not null} &&
            msAuthenticationStateProvider.ImplementationType.IsGenericType &&
            msAuthenticationStateProvider.ImplementationType.GetGenericTypeDefinition() == typeof(RemoteAuthenticationService<,,>))
        {
            var webAssemblyAuthenticationStateProviderType = typeof(WebAssemblyAuthenticationStateProvider<,,>).MakeGenericType(
                    msAuthenticationStateProvider.ImplementationType.GenericTypeArguments[0],
                    msAuthenticationStateProvider.ImplementationType.GenericTypeArguments[1],
                    msAuthenticationStateProvider.ImplementationType.GenericTypeArguments[2]);

            context.Services.Replace(ServiceDescriptor.Scoped(typeof(AuthenticationStateProvider), webAssemblyAuthenticationStateProviderType));
        }
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.ServiceProvider.GetRequiredService<IClientScopeServiceProviderAccessor>().ServiceProvider.GetRequiredService<WebAssemblyCachedApplicationConfigurationClient>().InitializeAsync();
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
