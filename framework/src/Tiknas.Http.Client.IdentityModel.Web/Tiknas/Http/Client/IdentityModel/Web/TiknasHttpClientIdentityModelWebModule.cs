using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;

namespace Tiknas.Http.Client.IdentityModel.Web;

[DependsOn(
    typeof(TiknasHttpClientIdentityModelModule)
    )]
public class TiknasHttpClientIdentityModelWebModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpContextAccessor();
    }
}
