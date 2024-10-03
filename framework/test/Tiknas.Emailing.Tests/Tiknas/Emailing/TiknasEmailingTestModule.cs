using Tiknas.Autofac;
using Tiknas.Modularity;
using Tiknas.VirtualFileSystem;

namespace Tiknas.Emailing;

[DependsOn(
    typeof(TiknasEmailingModule),
    typeof(TiknasAutofacModule),
    typeof(TiknasTestBaseModule))]
public class TiknasEmailingTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasEmailingTestModule>();
        });
    }
}
