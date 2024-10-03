using System;

namespace Tiknas.EventBus;

public interface IEventNameProvider
{
    string GetName(Type eventType);
}
