using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas.Domain;
using Tiknas.Domain.Repositories.MemoryDb;
using Tiknas.Modularity;
using Tiknas.Uow.MemoryDb;

namespace Tiknas.MemoryDb;

[DependsOn(typeof(TiknasDddDomainModule))]
public class TiknasMemoryDbModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.TryAddTransient(typeof(IMemoryDatabaseProvider<>), typeof(UnitOfWorkMemoryDatabaseProvider<>));
        context.Services.TryAddTransient(typeof(IMemoryDatabaseCollection<>), typeof(MemoryDatabaseCollection<>));
    }
}
