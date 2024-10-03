using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas.EntityFrameworkCore.GlobalFilters;

namespace Tiknas.EntityFrameworkCore;

public class TiknasDbContextOptionsExtension : IDbContextOptionsExtension
{
    public void ApplyServices(IServiceCollection services)
    {
        var serviceDescriptor = services.FirstOrDefault(x => x.ServiceType == typeof(ICompiledQueryCacheKeyGenerator));
        if (serviceDescriptor != null && serviceDescriptor.ImplementationType != null)
        {
            services.Remove(serviceDescriptor);
            services.AddScoped(serviceDescriptor.ImplementationType);
            services.Add(ServiceDescriptor.Scoped<ICompiledQueryCacheKeyGenerator>(provider =>
                ActivatorUtilities.CreateInstance<TiknasCompiledQueryCacheKeyGenerator>(provider,
                    provider.GetRequiredService(serviceDescriptor.ImplementationType)
                        .As<ICompiledQueryCacheKeyGenerator>())));
        }

        services.Replace(ServiceDescriptor.Scoped<IAsyncQueryProvider, TiknasEntityQueryProvider>());
        services.AddSingleton(typeof(TiknasEfCoreCurrentDbContext));
    }

    public void Validate(IDbContextOptions options)
    {
    }

    public DbContextOptionsExtensionInfo Info => new TiknasOptionsExtensionInfo(this);

    private class TiknasOptionsExtensionInfo : DbContextOptionsExtensionInfo
    {
        public TiknasOptionsExtensionInfo(IDbContextOptionsExtension extension)
            : base(extension)
        {
        }

        public override bool IsDatabaseProvider => false;

        public override int GetServiceProviderHashCode()
        {
            return 0;
        }

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
        {
            return other is TiknasOptionsExtensionInfo;
        }

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
        {
        }

        public override string LogFragment => "TiknasOptionsExtension";
    }
}
