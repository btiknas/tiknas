using Tiknas.EventBus.Distributed;

namespace Tiknas.EntityFrameworkCore.DistributedEvents;

public interface IDbContextEventInbox<TDbContext> : IEventInbox
    where TDbContext : IHasEventInbox
{

}
