using Microsoft.EntityFrameworkCore;

namespace Tiknas.EntityFrameworkCore.DistributedEvents;

public interface IHasEventInbox : IEfCoreDbContext
{
    DbSet<IncomingEventRecord> IncomingEvents { get; set; }
}
