using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;

namespace Tiknas.ApiVersioning;

public class TiknasApiVersioningAbstractionsModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IRequestedApiVersion>(NullRequestedApiVersion.Instance);
    }
}
