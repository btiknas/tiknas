using JetBrains.Annotations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas;
using Tiknas.AspNetCore.Components.WebAssembly.WebApp;
using Tiknas.Http.Client.Authentication;

namespace Microsoft.Extensions.DependencyInjection;

public static class TiknasBlazorWebAppServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorWebAppServices([NotNull] this IServiceCollection services)
    {
        Check.NotNull(services, nameof(services));

        services.AddSingleton<AuthenticationStateProvider, RemoteAuthenticationStateProvider>();
        services.Replace(ServiceDescriptor.Transient<ITiknasAccessTokenProvider, CookieBasedWebAssemblyTiknasAccessTokenProvider>());

        return services;
    }

    public static IServiceCollection AddBlazorWebAppTieredServices([NotNull] this IServiceCollection services)
    {
        Check.NotNull(services, nameof(services));

        services.AddScoped<AuthenticationStateProvider, RemoteAuthenticationStateProvider>();
        services.Replace(ServiceDescriptor.Singleton<ITiknasAccessTokenProvider, PersistentComponentStateTiknasAccessTokenProvider>());

        return services;
    }
}
