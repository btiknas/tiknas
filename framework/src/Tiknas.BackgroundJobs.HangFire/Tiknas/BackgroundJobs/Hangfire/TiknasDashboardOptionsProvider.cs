using System.Linq;
using System.Threading;
using Hangfire;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.BackgroundJobs.Hangfire;

public class TiknasDashboardOptionsProvider : ITransientDependency
{
    protected TiknasBackgroundJobOptions TiknasBackgroundJobOptions { get; }

    public TiknasDashboardOptionsProvider(IOptions<TiknasBackgroundJobOptions> tiknasBackgroundJobOptions)
    {
        TiknasBackgroundJobOptions = tiknasBackgroundJobOptions.Value;
    }

    public virtual DashboardOptions Get()
    {
        return new DashboardOptions
        {
            DisplayNameFunc = (_, job) =>
            {
                var jobName = job.ToString();
                if (job.Args.Count == 3 && job.Args.Last() is CancellationToken)
                {
                    jobName = TiknasBackgroundJobOptions.GetJob(job.Args[1].GetType()).JobName;
                }

                return jobName;
            }
        };
    }
}
