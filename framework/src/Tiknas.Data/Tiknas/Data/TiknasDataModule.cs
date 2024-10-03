using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.EventBus.Abstractions;
using Tiknas.Modularity;
using Tiknas.ObjectExtending;
using Tiknas.Uow;

namespace Tiknas.Data;

[DependsOn(
    typeof(TiknasObjectExtendingModule),
    typeof(TiknasUnitOfWorkModule),
    typeof(TiknasEventBusAbstractionsModule)
)]
public class TiknasDataModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AutoAddDataSeedContributors(context.Services);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<TiknasDbConnectionOptions>(configuration);

        context.Services.AddSingleton(typeof(IDataFilter<>), typeof(DataFilter<>));
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasDbConnectionOptions>(options =>
        {
            options.Databases.RefreshIndexes();
        });
    }

    private static void AutoAddDataSeedContributors(IServiceCollection services)
    {
        var contributors = new List<Type>();

        services.OnRegistered(context =>
        {
            if (typeof(IDataSeedContributor).IsAssignableFrom(context.ImplementationType))
            {
                contributors.Add(context.ImplementationType);
            }
        });

        services.Configure<TiknasDataSeedOptions>(options =>
        {
            options.Contributors.AddIfNotContains(contributors);
        });
    }
}
