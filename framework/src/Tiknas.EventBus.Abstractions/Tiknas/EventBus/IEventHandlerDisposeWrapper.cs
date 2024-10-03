using System;

namespace Tiknas.EventBus;

public interface IEventHandlerDisposeWrapper : IDisposable
{
    IEventHandler EventHandler { get; }
}
