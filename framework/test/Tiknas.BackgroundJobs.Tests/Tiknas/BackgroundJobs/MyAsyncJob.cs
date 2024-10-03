using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tiknas.DependencyInjection;
using Tiknas.MultiTenancy;
using Tiknas.Threading;

namespace Tiknas.BackgroundJobs;

public class MyAsyncJob : AsyncBackgroundJob<MyAsyncJobArgs>, ISingletonDependency
{
    public List<string> ExecutedValues { get; } = new List<string>();

    public Guid? TenantId { get; set; }

    private readonly ICurrentTenant _currentTenant;
    private readonly ICancellationTokenProvider _cancellationTokenProvider;

    public bool Canceled { get; set; }

    public MyAsyncJob(
        ICurrentTenant currentTenant,
        ICancellationTokenProvider cancellationTokenProvider)
    {
        _currentTenant = currentTenant;
        _cancellationTokenProvider = cancellationTokenProvider;
    }

    public override Task ExecuteAsync(MyAsyncJobArgs args)
    {
        if (_cancellationTokenProvider.Token.IsCancellationRequested)
        {
            Canceled = true;
        }

        ExecutedValues.Add(args.Value);
        TenantId = _currentTenant.Id;
        return Task.CompletedTask;
    }
}
