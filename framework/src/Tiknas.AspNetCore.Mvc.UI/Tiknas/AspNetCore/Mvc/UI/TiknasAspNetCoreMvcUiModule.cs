using Microsoft.Extensions.DependencyInjection;
using Tiknas.Modularity;
using Tiknas.UI.Navigation;
using Tiknas.VirtualFileSystem;

namespace Tiknas.AspNetCore.Mvc.UI;

[DependsOn(typeof(TiknasAspNetCoreMvcModule))]
[DependsOn(typeof(TiknasUiNavigationModule))]
public class TiknasAspNetCoreMvcUiModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(TiknasAspNetCoreMvcUiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasAspNetCoreMvcUiModule>();
        });
    }
}
