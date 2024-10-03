using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.VirtualFileSystem;
using Tiknas.AspNetCore.WebClientInfo;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore;

public class TiknasAspNetCoreAbstractionsModule
    : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IWebContentFileProvider, NullWebContentFileProvider>();
        context.Services.AddSingleton<IWebClientInfoProvider, NullWebClientInfoProvider>();;
    }
}
