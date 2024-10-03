using Tiknas.Modularity;
using Tiknas.VirtualFileSystem;

namespace Tiknas.TextTemplating.Scriban;

[DependsOn(
    typeof(TiknasTextTemplatingTestModule)
)]
public class ScribanTextTemplatingTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ScribanTextTemplatingTestModule>("Tiknas.TextTemplating.Scriban");
        });
    }
}
