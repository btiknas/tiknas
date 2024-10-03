using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Json;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.Reflection;

namespace Tiknas.BackgroundJobs;

[DependsOn(
    typeof(TiknasJsonModule),
    typeof(TiknasMultiTenancyAbstractionsModule)
    )]
public class TiknasBackgroundJobsAbstractionsModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        RegisterJobs(context.Services);
    }

    private static void RegisterJobs(IServiceCollection services)
    {
        var jobTypes = new List<Type>();

        services.OnRegistered(context =>
        {
            if (ReflectionHelper.IsAssignableToGenericType(context.ImplementationType, typeof(IBackgroundJob<>)) ||
                ReflectionHelper.IsAssignableToGenericType(context.ImplementationType, typeof(IAsyncBackgroundJob<>)))
            {
                jobTypes.Add(context.ImplementationType);
            }
        });

        services.Configure<TiknasBackgroundJobOptions>(options =>
        {
            foreach (var jobType in jobTypes)
            {
                options.AddJob(jobType);
            }
        });
    }
}
