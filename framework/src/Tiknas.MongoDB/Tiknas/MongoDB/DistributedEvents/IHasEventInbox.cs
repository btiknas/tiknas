using MongoDB.Driver;

namespace Tiknas.MongoDB.DistributedEvents;

public interface IHasEventInbox : ITiknasMongoDbContext
{
    IMongoCollection<IncomingEventRecord> IncomingEvents { get; }
}
