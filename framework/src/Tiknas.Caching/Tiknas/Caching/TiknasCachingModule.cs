using Microsoft.Extensions.DependencyInjection;
using System;
using Tiknas.Json;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.Serialization;
using Tiknas.Threading;
using Tiknas.Uow;

namespace Tiknas.Caching;

[DependsOn(
    typeof(TiknasThreadingModule),
    typeof(TiknasSerializationModule),
    typeof(TiknasUnitOfWorkModule),
    typeof(TiknasMultiTenancyModule),
    typeof(TiknasJsonModule))]
public class TiknasCachingModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMemoryCache();
        context.Services.AddDistributedMemoryCache();

        context.Services.AddSingleton(typeof(IDistributedCache<>), typeof(DistributedCache<>));
        context.Services.AddSingleton(typeof(IDistributedCache<,>), typeof(DistributedCache<,>));

        context.Services.Configure<TiknasDistributedCacheOptions>(cacheOptions =>
        {
            cacheOptions.GlobalCacheEntryOptions.SlidingExpiration = TimeSpan.FromMinutes(20);
        });
    }
}
