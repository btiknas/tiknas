using Tiknas.EventBus.Distributed;

namespace Tiknas.MongoDB.DistributedEvents;

public interface IMongoDbContextEventInbox<TDbContext> : IEventInbox
    where TDbContext : IHasEventInbox
{

}
