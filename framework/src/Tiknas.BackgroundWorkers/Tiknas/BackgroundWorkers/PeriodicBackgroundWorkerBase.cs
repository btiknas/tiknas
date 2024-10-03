using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tiknas.ExceptionHandling;
using Tiknas.Threading;

namespace Tiknas.BackgroundWorkers;

/// <summary>
/// Extends <see cref="BackgroundWorkerBase"/> to add a periodic running Timer.
/// </summary>
public abstract class PeriodicBackgroundWorkerBase : BackgroundWorkerBase
{
    protected IServiceScopeFactory ServiceScopeFactory { get; }
    protected TiknasTimer Timer { get; }

    protected PeriodicBackgroundWorkerBase(
        TiknasTimer timer,
        IServiceScopeFactory serviceScopeFactory)
    {
        ServiceScopeFactory = serviceScopeFactory;
        Timer = timer;
        Timer.Elapsed += Timer_Elapsed;
    }

    public override async Task StartAsync(CancellationToken cancellationToken = default)
    {
        await base.StartAsync(cancellationToken);
        Timer.Start(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken = default)
    {
        Timer.Stop(cancellationToken);
        await base.StopAsync(cancellationToken);
    }

    private void Timer_Elapsed(object? sender, System.EventArgs e)
    {
        using (var scope = ServiceScopeFactory.CreateScope())
        {
            try
            {
                DoWork(new PeriodicBackgroundWorkerContext(scope.ServiceProvider));
            }
            catch (Exception ex)
            {
                var exceptionNotifier = scope.ServiceProvider.GetRequiredService<IExceptionNotifier>();
                AsyncHelper.RunSync(() => exceptionNotifier.NotifyAsync(new ExceptionNotificationContext(ex)));

                Logger.LogException(ex);
            }
        }
    }

    /// <summary>
    /// Periodic works should be done by implementing this method.
    /// </summary>
    protected abstract void DoWork(PeriodicBackgroundWorkerContext workerContext);
}
