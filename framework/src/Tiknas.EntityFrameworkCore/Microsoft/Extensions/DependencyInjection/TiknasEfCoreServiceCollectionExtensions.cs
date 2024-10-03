using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas.DependencyInjection;
using Tiknas.EntityFrameworkCore;
using Tiknas.EntityFrameworkCore.DependencyInjection;
using Tiknas.MultiTenancy;

namespace Microsoft.Extensions.DependencyInjection;

public static class TiknasEfCoreServiceCollectionExtensions
{
    public static IServiceCollection AddTiknasDbContext<TDbContext>(
        this IServiceCollection services,
        Action<ITiknasDbContextRegistrationOptionsBuilder>? optionsBuilder = null)
        where TDbContext : TiknasDbContext<TDbContext>
    {
        services.AddMemoryCache();

        var options = new TiknasDbContextRegistrationOptions(typeof(TDbContext), services);

        var replacedMultiTenantDbContextTypes = typeof(TDbContext).GetCustomAttributes<ReplaceDbContextAttribute>(true)
            .SelectMany(x => x.ReplacedDbContextTypes).ToList();

        foreach (var dbContextType in replacedMultiTenantDbContextTypes)
        {
            options.ReplaceDbContext(dbContextType.Type, multiTenancySides: dbContextType.MultiTenancySide);
        }

        optionsBuilder?.Invoke(options);

        services.TryAddTransient(DbContextOptionsFactory.Create<TDbContext>);

        foreach (var entry in options.ReplacedDbContextTypes)
        {
            var originalDbContextType = entry.Key.Type;
            var targetDbContextType = entry.Value ?? typeof(TDbContext);

            services.Replace(ServiceDescriptor.Transient(originalDbContextType, sp =>
            {
                var dbContextType = sp.GetRequiredService<IEfCoreDbContextTypeProvider>().GetDbContextType(originalDbContextType);
                return sp.GetRequiredService(dbContextType);
            }));

            services.Configure<TiknasDbContextOptions>(opts =>
            {
                var multiTenantDbContextType = new MultiTenantDbContextType(originalDbContextType, entry.Key.MultiTenancySide);
                opts.DbContextReplacements[multiTenantDbContextType] = targetDbContextType;
            });
        }

        new EfCoreRepositoryRegistrar(options).AddRepositories();

        return services;
    }
}
