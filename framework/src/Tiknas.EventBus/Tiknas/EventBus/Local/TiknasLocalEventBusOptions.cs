using Tiknas.Collections;

namespace Tiknas.EventBus.Local;

public class TiknasLocalEventBusOptions

{
    public ITypeList<IEventHandler> Handlers { get; }

    public TiknasLocalEventBusOptions()
    {
        Handlers = new TypeList<IEventHandler>();
    }
}
