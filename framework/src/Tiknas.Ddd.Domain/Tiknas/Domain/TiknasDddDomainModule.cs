using Microsoft.Extensions.DependencyInjection;
using Tiknas.Auditing;
using Tiknas.Caching;
using Tiknas.Data;
using Tiknas.Domain.ChangeTracking;
using Tiknas.Domain.Repositories;
using Tiknas.EventBus;
using Tiknas.ExceptionHandling;
using Tiknas.Guids;
using Tiknas.Modularity;
using Tiknas.ObjectMapping;
using Tiknas.Specifications;
using Tiknas.Timing;

namespace Tiknas.Domain;

[DependsOn(
    typeof(TiknasAuditingModule),
    typeof(TiknasDataModule),
    typeof(TiknasEventBusModule),
    typeof(TiknasGuidsModule),
    typeof(TiknasTimingModule),
    typeof(TiknasObjectMappingModule),
    typeof(TiknasExceptionHandlingModule),
    typeof(TiknasSpecificationsModule),
    typeof(TiknasCachingModule),
    typeof(TiknasDddDomainSharedModule)
    )]
public class TiknasDddDomainModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddConventionalRegistrar(new TiknasRepositoryConventionalRegistrar());
        context.Services.OnRegistered(ChangeTrackingInterceptorRegistrar.RegisterIfNeeded);
    }
}
