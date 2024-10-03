﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Quartz;
using Tiknas.Json;

namespace Tiknas.BackgroundJobs.Quartz;

public class QuartzJobExecutionAdapter<TArgs> : IJob
{
    public ILogger<QuartzJobExecutionAdapter<TArgs>> Logger { get; set; }

    protected TiknasBackgroundJobOptions Options { get; }
    protected TiknasBackgroundJobQuartzOptions BackgroundJobQuartzOptions { get; }
    protected IServiceScopeFactory ServiceScopeFactory { get; }
    protected IBackgroundJobExecuter JobExecuter { get; }
    protected IJsonSerializer JsonSerializer { get; }

    public QuartzJobExecutionAdapter(
        IOptions<TiknasBackgroundJobOptions> options,
        IOptions<TiknasBackgroundJobQuartzOptions> backgroundJobQuartzOptions,
        IBackgroundJobExecuter jobExecuter,
        IServiceScopeFactory serviceScopeFactory,
        IJsonSerializer jsonSerializer)
    {
        JobExecuter = jobExecuter;
        ServiceScopeFactory = serviceScopeFactory;
        JsonSerializer = jsonSerializer;
        Options = options.Value;
        BackgroundJobQuartzOptions = backgroundJobQuartzOptions.Value;
        Logger = NullLogger<QuartzJobExecutionAdapter<TArgs>>.Instance;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = ServiceScopeFactory.CreateScope())
        {
            var args = JsonSerializer.Deserialize<TArgs>(context.JobDetail.JobDataMap.GetString(nameof(TArgs))!);
            var jobType = Options.GetJob(typeof(TArgs)).JobType;
            var jobContext = new JobExecutionContext(scope.ServiceProvider, jobType, args!, cancellationToken: context.CancellationToken);
            try
            {
                await JobExecuter.ExecuteAsync(jobContext);
            }
            catch (Exception exception)
            {
                var jobExecutionException = new JobExecutionException(exception);

                var retryIndex = context.JobDetail.JobDataMap.GetString(QuartzBackgroundJobManager.JobDataPrefix + QuartzBackgroundJobManager.RetryIndex)!.To<int>();
                retryIndex++;
                context.JobDetail.JobDataMap.Put(QuartzBackgroundJobManager.JobDataPrefix + QuartzBackgroundJobManager.RetryIndex, retryIndex.ToString());

                await BackgroundJobQuartzOptions.RetryStrategy.Invoke(retryIndex, context, jobExecutionException);

                throw jobExecutionException;
            }
        }
    }
}
