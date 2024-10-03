using Tiknas.EventBus.Distributed;

namespace Tiknas.EntityFrameworkCore.DistributedEvents;

public interface IDbContextEventOutbox<TDbContext> : IEventOutbox
    where TDbContext : IHasEventOutbox
{

}
