using Tiknas.AspNetCore.Mvc;
using Tiknas.Modularity;
using Tiknas.VirtualFileSystem;

namespace Tiknas.Swashbuckle;

[DependsOn(
    typeof(TiknasVirtualFileSystemModule),
    typeof(TiknasAspNetCoreMvcModule))]
public class TiknasSwashbuckleModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasSwashbuckleModule>();
        });
    }
}
