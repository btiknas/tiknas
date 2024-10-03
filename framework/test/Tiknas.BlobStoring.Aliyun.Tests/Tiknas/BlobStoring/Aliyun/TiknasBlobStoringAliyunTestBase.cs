using Tiknas.TestBase;

namespace Tiknas.BlobStoring.Aliyun;

public class TiknasBlobStoringAliyunTestCommonBase : TiknasIntegratedTest<TiknasBlobStoringAliyunTestCommonModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
public class TiknasBlobStoringAliyunTestBase : TiknasIntegratedTest<TiknasBlobStoringAliyunTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
