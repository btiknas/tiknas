using Microsoft.Extensions.DependencyInjection;
using Tiknas.Castle.DynamicProxy;
using Tiknas.Modularity;

namespace Tiknas.Castle;

public class TiknasCastleCoreModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient(typeof(TiknasAsyncDeterminationInterceptor<>));
    }
}
