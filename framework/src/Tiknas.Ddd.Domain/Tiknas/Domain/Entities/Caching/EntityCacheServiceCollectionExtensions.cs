using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas.Caching;
using Tiknas.Json.SystemTextJson;
using Tiknas.Json.SystemTextJson.Modifiers;

namespace Tiknas.Domain.Entities.Caching;

public static class EntityCacheServiceCollectionExtensions
{
    public static IServiceCollection AddEntityCache<TEntity, TKey>(
        this IServiceCollection services,
        DistributedCacheEntryOptions? cacheOptions = null)
        where TEntity : Entity<TKey>
    {
        services.TryAddTransient<IEntityCache<TEntity, TKey>, EntityCacheWithoutCacheItem<TEntity, TKey>>();
        services.TryAddTransient<EntityCacheWithoutCacheItem<TEntity, TKey>>();

        services.Configure<TiknasDistributedCacheOptions>(options =>
        {
            options.ConfigureCache<TEntity>(cacheOptions ?? GetDefaultCacheOptions());
        });

        services.Configure<TiknasSystemTextJsonSerializerModifiersOptions>(options =>
        {
            options.Modifiers.Add(new TiknasIncludeNonPublicPropertiesModifiers<TEntity, TKey>().CreateModifyAction(x => x.Id));
        });

        return services;
    }

    public static IServiceCollection AddEntityCache<TEntity, TEntityCacheItem, TKey>(
        this IServiceCollection services,
        DistributedCacheEntryOptions? cacheOptions = null)
        where TEntity : Entity<TKey>
        where TEntityCacheItem : class
    {
        services.TryAddTransient<IEntityCache<TEntityCacheItem, TKey>, EntityCacheWithObjectMapper<TEntity, TEntityCacheItem, TKey>>();
        services.TryAddTransient<EntityCacheWithObjectMapper<TEntity, TEntityCacheItem, TKey>>();

        services.Configure<TiknasDistributedCacheOptions>(options =>
        {
            options.ConfigureCache<TEntityCacheItem>(cacheOptions ?? GetDefaultCacheOptions());
        });

        return services;
    }

    public static IServiceCollection AddEntityCache<TObjectMapperContext, TEntity, TEntityCacheItem, TKey>(
        this IServiceCollection services,
        DistributedCacheEntryOptions? cacheOptions = null)
        where TEntity : Entity<TKey>
        where TEntityCacheItem : class
    {
        services.TryAddTransient<IEntityCache<TEntityCacheItem, TKey>, EntityCacheWithObjectMapperContext<TObjectMapperContext, TEntity, TEntityCacheItem, TKey>>();
        services.TryAddTransient<EntityCacheWithObjectMapperContext<TObjectMapperContext, TEntity, TEntityCacheItem, TKey>>();

        services.Configure<TiknasDistributedCacheOptions>(options =>
        {
            options.ConfigureCache<TEntityCacheItem>(cacheOptions ?? GetDefaultCacheOptions());
        });

        return services;
    }

    private static DistributedCacheEntryOptions GetDefaultCacheOptions()
    {
        return new DistributedCacheEntryOptions {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
        };
    }
}
