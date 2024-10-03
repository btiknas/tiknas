using Tiknas.Caching;
using Tiknas.Modularity;

namespace Tiknas.BlobStoring.Aliyun;

[DependsOn(
    typeof(TiknasBlobStoringModule),
    typeof(TiknasCachingModule)
    )]
public class TiknasBlobStoringAliyunModule : TiknasModule
{

}
