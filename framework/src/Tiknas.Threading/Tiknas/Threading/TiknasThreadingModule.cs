using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;

namespace Tiknas.Threading;

public class TiknasThreadingModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<ICancellationTokenProvider>(NullCancellationTokenProvider.Instance);
        context.Services.AddSingleton(typeof(IAmbientScopeProvider<>), typeof(AmbientDataContextAmbientScopeProvider<>));
    }
}
