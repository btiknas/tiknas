using Tiknas.EventBus.Distributed;

namespace Tiknas.MongoDB.DistributedEvents;

public interface IMongoDbContextEventOutbox<TDbContext> : IEventOutbox
    where TDbContext : IHasEventOutbox
{

}
