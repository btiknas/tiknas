﻿using System;

namespace Tiknas.BackgroundJobs;

public class BackgroundJobConfiguration
{
    public Type ArgsType { get; }

    public Type JobType { get; }

    public string JobName { get; }

    public BackgroundJobConfiguration(Type jobType)
    {
        JobType = jobType;
        ArgsType = BackgroundJobArgsHelper.GetJobArgsType(jobType);
        JobName = BackgroundJobNameAttribute.GetName(ArgsType);
    }
}
