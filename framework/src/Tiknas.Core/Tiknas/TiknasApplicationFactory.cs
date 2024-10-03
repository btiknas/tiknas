using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;

namespace Tiknas;

public static class TiknasApplicationFactory
{
    public async static Task<ITiknasApplicationWithInternalServiceProvider> CreateAsync<TStartupModule>(
        Action<TiknasApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : ITiknasModule
    {
        var app = Create(typeof(TStartupModule), options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    public async static Task<ITiknasApplicationWithInternalServiceProvider> CreateAsync(
        [NotNull] Type startupModuleType,
        Action<TiknasApplicationCreationOptions>? optionsAction = null)
    {
        var app = new TiknasApplicationWithInternalServiceProvider(startupModuleType, options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    public async static Task<ITiknasApplicationWithExternalServiceProvider> CreateAsync<TStartupModule>(
        [NotNull] IServiceCollection services,
        Action<TiknasApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : ITiknasModule
    {
        var app = Create(typeof(TStartupModule), services, options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    public async static Task<ITiknasApplicationWithExternalServiceProvider> CreateAsync(
        [NotNull] Type startupModuleType,
        [NotNull] IServiceCollection services,
        Action<TiknasApplicationCreationOptions>? optionsAction = null)
    {
        var app = new TiknasApplicationWithExternalServiceProvider(startupModuleType, services, options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    public static ITiknasApplicationWithInternalServiceProvider Create<TStartupModule>(
        Action<TiknasApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : ITiknasModule
    {
        return Create(typeof(TStartupModule), optionsAction);
    }

    public static ITiknasApplicationWithInternalServiceProvider Create(
        [NotNull] Type startupModuleType,
        Action<TiknasApplicationCreationOptions>? optionsAction = null)
    {
        return new TiknasApplicationWithInternalServiceProvider(startupModuleType, optionsAction);
    }

    public static ITiknasApplicationWithExternalServiceProvider Create<TStartupModule>(
        [NotNull] IServiceCollection services,
        Action<TiknasApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : ITiknasModule
    {
        return Create(typeof(TStartupModule), services, optionsAction);
    }

    public static ITiknasApplicationWithExternalServiceProvider Create(
        [NotNull] Type startupModuleType,
        [NotNull] IServiceCollection services,
        Action<TiknasApplicationCreationOptions>? optionsAction = null)
    {
        return new TiknasApplicationWithExternalServiceProvider(startupModuleType, services, optionsAction);
    }
}
