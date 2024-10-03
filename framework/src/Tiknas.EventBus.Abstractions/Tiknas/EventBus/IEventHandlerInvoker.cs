using System;
using System.Threading.Tasks;

namespace Tiknas.EventBus;

public interface IEventHandlerInvoker
{
    Task InvokeAsync(IEventHandler eventHandler, object eventData, Type eventType);
}
