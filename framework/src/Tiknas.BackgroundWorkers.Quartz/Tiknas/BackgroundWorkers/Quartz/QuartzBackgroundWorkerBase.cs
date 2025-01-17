﻿using System;
using System.Threading.Tasks;
using Quartz;

namespace Tiknas.BackgroundWorkers.Quartz;

public abstract class QuartzBackgroundWorkerBase : BackgroundWorkerBase, IQuartzBackgroundWorker
{
    public ITrigger Trigger { get; set; } = default!;

    public IJobDetail JobDetail { get; set; } = default!;

    public bool AutoRegister { get; set; } = true;

    public Func<IScheduler, Task>? ScheduleJob { get; set; } = null;

    public abstract Task Execute(IJobExecutionContext context);
}
