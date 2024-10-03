using Tiknas.TestBase;

namespace Tiknas.BlobStoring.Minio;

public class TiknasBlobStoringMinioTestCommonBase : TiknasIntegratedTest<TiknasBlobStoringMinioTestCommonModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}

public class TiknasBlobStoringMinioTestBase : TiknasIntegratedTest<TiknasBlobStoringMinioTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
