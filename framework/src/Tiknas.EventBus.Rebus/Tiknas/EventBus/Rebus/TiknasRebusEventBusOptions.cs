﻿using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Transport.InMem;

namespace Tiknas.EventBus.Rebus;

public class TiknasRebusEventBusOptions
{
    [NotNull]
    public string InputQueueName { get; set; } = default!;

    [NotNull]
    public Action<RebusConfigurer> Configurer {
        get => _configurer;
        set => _configurer = Check.NotNull(value, nameof(value));
    }
    private Action<RebusConfigurer> _configurer;

    public Func<IBus, Type, object, Task>? Publish { get; set; }

    public TiknasRebusEventBusOptions()
    {
        _configurer = DefaultConfigure;
    }

    private void DefaultConfigure(RebusConfigurer configure)
    {
        configure.Subscriptions(s => s.StoreInMemory());
        configure.Transport(t => t.UseInMemoryTransport(new InMemNetwork(), InputQueueName));
    }
}
