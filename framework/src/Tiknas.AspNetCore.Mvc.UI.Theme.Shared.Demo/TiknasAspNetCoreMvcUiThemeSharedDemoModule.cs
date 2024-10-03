using Tiknas.Modularity;
using Tiknas.VirtualFileSystem;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Demo;

[DependsOn(
    typeof(TiknasAspNetCoreMvcUiThemeSharedModule)
    )]
public class TiknasAspNetCoreMvcUiThemeSharedDemoModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasAspNetCoreMvcUiThemeSharedDemoModule>();
        });
    }
}
