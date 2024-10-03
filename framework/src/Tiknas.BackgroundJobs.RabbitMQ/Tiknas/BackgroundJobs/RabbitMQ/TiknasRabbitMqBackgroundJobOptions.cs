using System;
using System.Collections.Generic;

namespace Tiknas.BackgroundJobs.RabbitMQ;

public class TiknasRabbitMqBackgroundJobOptions
{
    /// <summary>
    /// Key: Job Args Type
    /// </summary>
    public Dictionary<Type, JobQueueConfiguration> JobQueues { get; }

    /// <summary>
    /// Default value: "TiknasBackgroundJobs.".
    /// </summary>
    public string DefaultQueueNamePrefix { get; set; }

    /// <summary>
    /// Default value: "TiknasBackgroundJobsDelayed."
    /// </summary>
    public string DefaultDelayedQueueNamePrefix { get; set; }
    
    public ushort? PrefetchCount { get; set; }

    public TiknasRabbitMqBackgroundJobOptions()
    {
        JobQueues = new Dictionary<Type, JobQueueConfiguration>();
        DefaultQueueNamePrefix = "TiknasBackgroundJobs.";
        DefaultDelayedQueueNamePrefix = "TiknasBackgroundJobsDelayed.";
    }
}
