using Microsoft.Extensions.Caching.Distributed;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas.Modularity;

namespace Tiknas.Caching;

[DependsOn(typeof(TiknasCachingModule))]
public class TiknasCachingTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasDistributedCacheOptions>(option =>
        {
            option.CacheConfigurators.Add(cacheName =>
            {
                if (cacheName == CacheNameAttribute.GetCacheName(typeof(Sail.Testing.Caching.PersonCacheItem)))
                {
                    return new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTime.Parse("2099-01-01 12:00:00")
                    };
                }

                return null;
            });

            option.GlobalCacheEntryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(20));
        });

        context.Services.Replace(ServiceDescriptor.Singleton<IDistributedCache, TestMemoryDistributedCache>());
    }
}
