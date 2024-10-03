using Microsoft.EntityFrameworkCore;

namespace Tiknas.EntityFrameworkCore.DistributedEvents;

public interface IHasEventOutbox : IEfCoreDbContext
{
    DbSet<OutgoingEventRecord> OutgoingEvents { get; set; }
}
