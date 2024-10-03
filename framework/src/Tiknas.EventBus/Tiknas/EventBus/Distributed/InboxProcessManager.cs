using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.BackgroundWorkers;

namespace Tiknas.EventBus.Distributed;

public class InboxProcessManager : IBackgroundWorker
{
    protected TiknasDistributedEventBusOptions Options { get; }
    protected IServiceProvider ServiceProvider { get; }
    protected List<IInboxProcessor> Processors { get; }

    public InboxProcessManager(
        IOptions<TiknasDistributedEventBusOptions> options,
        IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        Options = options.Value;
        Processors = new List<IInboxProcessor>();
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        foreach (var inboxConfig in Options.Inboxes.Values)
        {
            if (inboxConfig.IsProcessingEnabled)
            {
                var processor = ServiceProvider.GetRequiredService<IInboxProcessor>();
                await processor.StartAsync(inboxConfig, cancellationToken);
                Processors.Add(processor);
            }
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        foreach (var processor in Processors)
        {
            await processor.StopAsync(cancellationToken);
        }
    }
}
