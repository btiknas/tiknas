using MongoDB.Driver;

namespace Tiknas.MongoDB.DistributedEvents;

public interface IHasEventOutbox : ITiknasMongoDbContext
{
    IMongoCollection<OutgoingEventRecord> OutgoingEvents { get; }
}
