using Tiknas.Caching;
using Tiknas.Domain.Repositories;
using Tiknas.Uow;

namespace Tiknas.Domain.Entities.Caching;

public class EntityCacheWithoutCacheItem<TEntity, TKey> :
    EntityCacheBase<TEntity, TEntity, TKey>
    where TEntity : Entity<TKey>
{
    public EntityCacheWithoutCacheItem(
        IReadOnlyRepository<TEntity, TKey> repository,
        IDistributedCache<TEntity, TKey> cache,
        IUnitOfWorkManager unitOfWorkManager)
        : base(repository, cache, unitOfWorkManager)
    {
    }

    protected override TEntity? MapToCacheItem(TEntity? entity)
    {
        return entity;
    }
}
