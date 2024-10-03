using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas.MemoryDb;
using Tiknas.MemoryDb.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class TiknasMemoryDbServiceCollectionExtensions
{
    public static IServiceCollection AddMemoryDbContext<TMemoryDbContext>(this IServiceCollection services, Action<ITiknasMemoryDbContextRegistrationOptionsBuilder>? optionsBuilder = null)
        where TMemoryDbContext : MemoryDbContext
    {
        var options = new TiknasMemoryDbContextRegistrationOptions(typeof(TMemoryDbContext), services);
        optionsBuilder?.Invoke(options);

        if (options.DefaultRepositoryDbContextType != typeof(TMemoryDbContext))
        {
            services.TryAddSingleton(options.DefaultRepositoryDbContextType, sp => sp.GetRequiredService<TMemoryDbContext>());
        }

        foreach (var entry in options.ReplacedDbContextTypes)
        {
            var originalDbContextType = entry.Key.Type;
            var targetDbContextType = entry.Value ?? typeof(TMemoryDbContext);

            services.Replace(
                ServiceDescriptor.Singleton(
                    originalDbContextType,
                    sp => sp.GetRequiredService(targetDbContextType)
                )
            );
        }

        new MemoryDbRepositoryRegistrar(options).AddRepositories();

        return services;
    }
}
