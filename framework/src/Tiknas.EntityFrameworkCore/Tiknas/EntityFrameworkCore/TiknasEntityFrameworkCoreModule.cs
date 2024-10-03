using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas.Domain;
using Tiknas.EntityFrameworkCore.DistributedEvents;
using Tiknas.Modularity;
using Tiknas.Uow.EntityFrameworkCore;

namespace Tiknas.EntityFrameworkCore;

[DependsOn(typeof(TiknasDddDomainModule))]
public class TiknasEntityFrameworkCoreModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasDbContextOptions>(options =>
        {
            options.PreConfigure(tiknasDbContextConfigurationContext =>
            {
                tiknasDbContextConfigurationContext.DbContextOptions
                    .ConfigureWarnings(warnings =>
                    {
                        warnings.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning);
                    });
            });
        });

        context.Services.TryAddTransient(typeof(IDbContextProvider<>), typeof(UnitOfWorkDbContextProvider<>));
        context.Services.AddTransient(typeof(IDbContextEventOutbox<>), typeof(DbContextEventOutbox<>));
        context.Services.AddTransient(typeof(IDbContextEventInbox<>), typeof(DbContextEventInbox<>));
    }
}
