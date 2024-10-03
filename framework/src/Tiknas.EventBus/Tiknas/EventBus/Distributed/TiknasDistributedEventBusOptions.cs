using Tiknas.Collections;

namespace Tiknas.EventBus.Distributed;

public class TiknasDistributedEventBusOptions
{
    public ITypeList<IEventHandler> Handlers { get; }

    public OutboxConfigDictionary Outboxes { get; }

    public InboxConfigDictionary Inboxes { get; }
    public TiknasDistributedEventBusOptions()
    {
        Handlers = new TypeList<IEventHandler>();
        Outboxes = new OutboxConfigDictionary();
        Inboxes = new InboxConfigDictionary();
    }
}
