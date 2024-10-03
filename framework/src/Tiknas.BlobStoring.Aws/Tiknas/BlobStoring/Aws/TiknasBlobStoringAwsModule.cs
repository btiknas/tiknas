using Tiknas.Caching;
using Tiknas.Modularity;

namespace Tiknas.BlobStoring.Aws;

[DependsOn(typeof(TiknasBlobStoringModule),
    typeof(TiknasCachingModule))]
public class TiknasBlobStoringAwsModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}
