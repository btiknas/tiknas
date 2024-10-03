using System;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.DependencyInjection;

namespace Tiknas.Data;

public static class TiknasDataMigrationEnvironmentExtensions
{
    public static void AddDataMigrationEnvironment(this TiknasApplicationCreationOptions options, TiknasDataMigrationEnvironment? environment = null)
    {
        options.Services.AddDataMigrationEnvironment(environment ?? new TiknasDataMigrationEnvironment());
    }

    public static void AddDataMigrationEnvironment(this IServiceCollection services, TiknasDataMigrationEnvironment? environment = null)
    {
        services.AddObjectAccessor<TiknasDataMigrationEnvironment>(environment ?? new TiknasDataMigrationEnvironment());
    }

    public static TiknasDataMigrationEnvironment? GetDataMigrationEnvironment(this IServiceCollection services)
    {
        return services.GetObjectOrNull<TiknasDataMigrationEnvironment>();
    }

    public static bool IsDataMigrationEnvironment(this IServiceCollection services)
    {
        return services.GetDataMigrationEnvironment() != null;
    }

    public static TiknasDataMigrationEnvironment? GetDataMigrationEnvironment(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<IObjectAccessor<TiknasDataMigrationEnvironment>>()?.Value;
    }

    public static bool IsDataMigrationEnvironment(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetDataMigrationEnvironment() != null;
    }
}
