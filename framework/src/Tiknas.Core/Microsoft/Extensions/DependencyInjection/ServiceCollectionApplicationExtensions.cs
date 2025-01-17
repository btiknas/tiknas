﻿using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Tiknas;
using Tiknas.Modularity;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionApplicationExtensions
{
    public static ITiknasApplicationWithExternalServiceProvider AddApplication<TStartupModule>(
        [NotNull] this IServiceCollection services,
        Action<TiknasApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : ITiknasModule
    {
        return TiknasApplicationFactory.Create<TStartupModule>(services, optionsAction);
    }

    public static ITiknasApplicationWithExternalServiceProvider AddApplication(
        [NotNull] this IServiceCollection services,
        [NotNull] Type startupModuleType,
        Action<TiknasApplicationCreationOptions>? optionsAction = null)
    {
        return TiknasApplicationFactory.Create(startupModuleType, services, optionsAction);
    }

    public async static Task<ITiknasApplicationWithExternalServiceProvider> AddApplicationAsync<TStartupModule>(
        [NotNull] this IServiceCollection services,
        Action<TiknasApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : ITiknasModule
    {
        return await TiknasApplicationFactory.CreateAsync<TStartupModule>(services,  optionsAction);
    }

    public async static Task<ITiknasApplicationWithExternalServiceProvider> AddApplicationAsync(
        [NotNull] this IServiceCollection services,
        [NotNull] Type startupModuleType,
        Action<TiknasApplicationCreationOptions>? optionsAction = null)
    {
        return await TiknasApplicationFactory.CreateAsync(startupModuleType, services, optionsAction);
    }

    public static string? GetApplicationName(this IServiceCollection services)
    {
        return services.GetSingletonInstance<IApplicationInfoAccessor>().ApplicationName;
    }

    [NotNull]
    public static string GetApplicationInstanceId(this IServiceCollection services)
    {
        return services.GetSingletonInstance<IApplicationInfoAccessor>().InstanceId;
    }

    [NotNull]
    public static ITiknasHostEnvironment GetTiknasHostEnvironment(this IServiceCollection services)
    {
        return services.GetSingletonInstance<ITiknasHostEnvironment>();
    }
}
