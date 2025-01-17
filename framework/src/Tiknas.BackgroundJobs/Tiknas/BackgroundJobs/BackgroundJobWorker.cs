﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tiknas.BackgroundWorkers;
using Tiknas.DistributedLocking;
using Tiknas.Threading;
using Tiknas.Timing;

namespace Tiknas.BackgroundJobs;

public class BackgroundJobWorker : AsyncPeriodicBackgroundWorkerBase, IBackgroundJobWorker
{
    protected const string DistributedLockName = "TiknasBackgroundJobWorker";

    protected TiknasBackgroundJobOptions JobOptions { get; }

    protected TiknasBackgroundJobWorkerOptions WorkerOptions { get; }

    protected ITiknasDistributedLock DistributedLock { get; }

    public BackgroundJobWorker(
        TiknasAsyncTimer timer,
        IOptions<TiknasBackgroundJobOptions> jobOptions,
        IOptions<TiknasBackgroundJobWorkerOptions> workerOptions,
        IServiceScopeFactory serviceScopeFactory,
        ITiknasDistributedLock distributedLock)
        : base(
            timer,
            serviceScopeFactory)
    {
        DistributedLock = distributedLock;
        WorkerOptions = workerOptions.Value;
        JobOptions = jobOptions.Value;
        Timer.Period = WorkerOptions.JobPollPeriod;
    }

    protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        await using (var handler = await DistributedLock.TryAcquireAsync(DistributedLockName, cancellationToken: StoppingToken))
        {
            if (handler != null)
            {
                var store = workerContext.ServiceProvider.GetRequiredService<IBackgroundJobStore>();

                var waitingJobs = await store.GetWaitingJobsAsync(WorkerOptions.MaxJobFetchCount);

                if (!waitingJobs.Any())
                {
                    return;
                }

                var jobExecuter = workerContext.ServiceProvider.GetRequiredService<IBackgroundJobExecuter>();
                var clock = workerContext.ServiceProvider.GetRequiredService<IClock>();
                var serializer = workerContext.ServiceProvider.GetRequiredService<IBackgroundJobSerializer>();

                foreach (var jobInfo in waitingJobs)
                {
                    jobInfo.TryCount++;
                    jobInfo.LastTryTime = clock.Now;

                    try
                    {
                        var jobConfiguration = JobOptions.GetJob(jobInfo.JobName);
                        var jobArgs = serializer.Deserialize(jobInfo.JobArgs, jobConfiguration.ArgsType);
                        var context = new JobExecutionContext(
                            workerContext.ServiceProvider,
                            jobConfiguration.JobType,
                            jobArgs,
                            workerContext.CancellationToken);

                        try
                        {
                            await jobExecuter.ExecuteAsync(context);

                            await store.DeleteAsync(jobInfo.Id);
                        }
                        catch (BackgroundJobExecutionException)
                        {
                            var nextTryTime = CalculateNextTryTime(jobInfo, clock);

                            if (nextTryTime.HasValue)
                            {
                                jobInfo.NextTryTime = nextTryTime.Value;
                            }
                            else
                            {
                                jobInfo.IsAbandoned = true;
                            }

                            await TryUpdateAsync(store, jobInfo);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex);
                        jobInfo.IsAbandoned = true;
                        await TryUpdateAsync(store, jobInfo);
                    }
                }
            }
            else
            {
                try
                {
                    await Task.Delay(WorkerOptions.JobPollPeriod * 12, StoppingToken);
                }
                catch (TaskCanceledException) { }
            }
        }
    }

    protected virtual async Task TryUpdateAsync(IBackgroundJobStore store, BackgroundJobInfo jobInfo)
    {
        try
        {
            await store.UpdateAsync(jobInfo);
        }
        catch (Exception updateEx)
        {
            Logger.LogException(updateEx);
        }
    }

    protected virtual DateTime? CalculateNextTryTime(BackgroundJobInfo jobInfo, IClock clock)
    {
        var nextWaitDuration = WorkerOptions.DefaultFirstWaitDuration *
                               (Math.Pow(WorkerOptions.DefaultWaitFactor, jobInfo.TryCount - 1));
        var nextTryDate = jobInfo.LastTryTime?.AddSeconds(nextWaitDuration) ??
                          clock.Now.AddSeconds(nextWaitDuration);

        if (nextTryDate.Subtract(jobInfo.CreationTime).TotalSeconds > WorkerOptions.DefaultTimeout)
        {
            return null;
        }

        return nextTryDate;
    }
}
