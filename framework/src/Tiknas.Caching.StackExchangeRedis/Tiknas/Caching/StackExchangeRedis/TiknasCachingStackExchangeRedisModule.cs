using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas.Modularity;

namespace Tiknas.Caching.StackExchangeRedis;

[DependsOn(
    typeof(TiknasCachingModule)
    )]
public class TiknasCachingStackExchangeRedisModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        var redisEnabled = configuration["Redis:IsEnabled"];
        if (string.IsNullOrEmpty(redisEnabled) || bool.Parse(redisEnabled))
        {
            context.Services.AddStackExchangeRedisCache(options =>
            {
                var redisConfiguration = configuration["Redis:Configuration"];
                if (!redisConfiguration.IsNullOrEmpty())
                {
                    options.Configuration = redisConfiguration;
                }
            });

            context.Services.Replace(ServiceDescriptor.Singleton<IDistributedCache, TiknasRedisCache>());
        }
    }
}
