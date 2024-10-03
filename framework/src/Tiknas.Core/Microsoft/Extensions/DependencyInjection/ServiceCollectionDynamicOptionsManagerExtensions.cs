using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Tiknas.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionDynamicOptionsManagerExtensions
{
    public static IServiceCollection AddTiknasDynamicOptions<TOptions, TManager>(this IServiceCollection services)
        where TOptions : class
        where TManager : TiknasDynamicOptionsManager<TOptions>
    {
        services.Replace(ServiceDescriptor.Scoped(typeof(IOptions<TOptions>), typeof(TManager)));
        services.Replace(ServiceDescriptor.Scoped(typeof(IOptionsSnapshot<TOptions>), typeof(TManager)));

        return services;
    }
}
