using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas.Domain;
using Tiknas.Domain.Repositories.MongoDB;
using Tiknas.Modularity;
using Tiknas.MongoDB.DependencyInjection;
using Tiknas.Uow.MongoDB;
using Tiknas.MongoDB.DistributedEvents;

namespace Tiknas.MongoDB;

[DependsOn(typeof(TiknasDddDomainModule))]
public class TiknasMongoDbModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddConventionalRegistrar(new TiknasMongoDbConventionalRegistrar());
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.TryAddTransient(
            typeof(IMongoDbContextProvider<>),
            typeof(UnitOfWorkMongoDbContextProvider<>)
        );

        context.Services.TryAddTransient(
            typeof(IMongoDbRepositoryFilterer<>),
            typeof(MongoDbRepositoryFilterer<>)
        );

        context.Services.TryAddTransient(
            typeof(IMongoDbRepositoryFilterer<,>),
            typeof(MongoDbRepositoryFilterer<,>)
        );

        context.Services.AddTransient(
            typeof(IMongoDbContextEventOutbox<>),
            typeof(MongoDbContextEventOutbox<>)
        );

        context.Services.AddTransient(
            typeof(IMongoDbContextEventInbox<>),
            typeof(MongoDbContextEventInbox<>)
        );
    }
}
