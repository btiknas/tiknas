using Tiknas.Data;
using Tiknas.EventBus.Distributed;

namespace Tiknas.MongoDB.DistributedEvents;

public static class MongoDbOutboxConfigExtensions
{
    public static void UseMongoDbContext<TMongoDbContext>(this OutboxConfig outboxConfig)
        where TMongoDbContext : IHasEventOutbox
    {
        outboxConfig.ImplementationType = typeof(IMongoDbContextEventOutbox<TMongoDbContext>);
        outboxConfig.DatabaseName = ConnectionStringNameAttribute.GetConnStringName<TMongoDbContext>();
    }
}
