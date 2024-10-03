using Tiknas.Modularity;
using Tiknas.VirtualFileSystem;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap;

[DependsOn(typeof(TiknasAspNetCoreMvcUiModule))]
public class TiknasAspNetCoreMvcUiBootstrapModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasAspNetCoreMvcUiBootstrapModule>("Tiknas.AspNetCore.Mvc.UI.Bootstrap");
        });
    }
}
