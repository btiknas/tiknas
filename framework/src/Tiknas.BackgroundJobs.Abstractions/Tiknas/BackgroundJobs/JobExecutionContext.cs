using System;
using System.Threading;
using Tiknas.DependencyInjection;

namespace Tiknas.BackgroundJobs;

public class JobExecutionContext : IServiceProviderAccessor
{
    public IServiceProvider ServiceProvider { get; }

    public Type JobType { get; }

    public object JobArgs { get; }

    public CancellationToken CancellationToken { get; }

    public JobExecutionContext(
        IServiceProvider serviceProvider,
        Type jobType,
        object jobArgs,
        CancellationToken cancellationToken = default)
    {
        ServiceProvider = serviceProvider;
        JobType = jobType;
        JobArgs = jobArgs;
        CancellationToken = cancellationToken;
    }
}
