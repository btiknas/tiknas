using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tiknas.ExceptionHandling;
using Tiknas.Threading;

namespace Tiknas.BackgroundWorkers;

public abstract class AsyncPeriodicBackgroundWorkerBase : BackgroundWorkerBase
{
    protected IServiceScopeFactory ServiceScopeFactory { get; }
    protected TiknasAsyncTimer Timer { get; }
    protected CancellationToken StartCancellationToken { get; set; }

    protected AsyncPeriodicBackgroundWorkerBase(
        TiknasAsyncTimer timer,
        IServiceScopeFactory serviceScopeFactory)
    {
        ServiceScopeFactory = serviceScopeFactory;
        Timer = timer;
        Timer.Elapsed = Timer_Elapsed;
    }

    public async override Task StartAsync(CancellationToken cancellationToken = default)
    {
        StartCancellationToken = cancellationToken;

        await base.StartAsync(cancellationToken);
        Timer.Start(cancellationToken);
    }

    public async override Task StopAsync(CancellationToken cancellationToken = default)
    {
        Timer.Stop(cancellationToken);
        await base.StopAsync(cancellationToken);
    }

    private async Task Timer_Elapsed(TiknasAsyncTimer timer)
    {
        await DoWorkAsync(StartCancellationToken);
    }

    private async Task DoWorkAsync(CancellationToken cancellationToken = default)
    {
        using (var scope = ServiceScopeFactory.CreateScope())
        {
            try
            {
                await DoWorkAsync(new PeriodicBackgroundWorkerContext(scope.ServiceProvider, cancellationToken));
            }
            catch (Exception ex)
            {
                await scope.ServiceProvider
                    .GetRequiredService<IExceptionNotifier>()
                    .NotifyAsync(new ExceptionNotificationContext(ex));

                Logger.LogException(ex);
            }
        }
    }

    protected abstract Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext);
}
