using Microsoft.Extensions.DependencyInjection;
using Tiknas.Http.Client;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;

namespace Tiknas.RemoteServices;

[DependsOn(typeof(TiknasMultiTenancyAbstractionsModule))]
public class TiknasRemoteServicesModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<TiknasRemoteServiceOptions>(configuration);
    }
}