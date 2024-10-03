using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;

namespace Tiknas.Uow;

public class TiknasUnitOfWorkModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnRegistered(UnitOfWorkInterceptorRegistrar.RegisterIfNeeded);
    }
}
