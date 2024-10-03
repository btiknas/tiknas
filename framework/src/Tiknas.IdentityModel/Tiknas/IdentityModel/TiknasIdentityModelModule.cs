using Microsoft.Extensions.DependencyInjection;
using Tiknas.Caching;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.Threading;

namespace Tiknas.IdentityModel;

[DependsOn(
    typeof(TiknasThreadingModule),
    typeof(TiknasMultiTenancyModule),
    typeof(TiknasCachingModule)
    )]
public class TiknasIdentityModelModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        context.Services.AddHttpClient(IdentityModelAuthenticationService.HttpClientName);

        Configure<TiknasIdentityClientOptions>(configuration);
    }
}
