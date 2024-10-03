using Tiknas.Caching;
using Tiknas.Domain.Repositories;
using Tiknas.ObjectMapping;
using Tiknas.Uow;

namespace Tiknas.Domain.Entities.Caching;

public class EntityCacheWithObjectMapperContext<TObjectMapperContext, TEntity, TEntityCacheItem, TKey> :
    EntityCacheWithObjectMapper<TEntity, TEntityCacheItem, TKey>
    where TEntity : Entity<TKey>
    where TEntityCacheItem : class
{
    public EntityCacheWithObjectMapperContext(
        IReadOnlyRepository<TEntity, TKey> repository,
        IDistributedCache<TEntityCacheItem, TKey> cache,
        IUnitOfWorkManager unitOfWorkManager,
        IObjectMapper objectMapper)// Intentionally injected with TContext
        : base(repository, cache, unitOfWorkManager, objectMapper)
    {

    }
}
