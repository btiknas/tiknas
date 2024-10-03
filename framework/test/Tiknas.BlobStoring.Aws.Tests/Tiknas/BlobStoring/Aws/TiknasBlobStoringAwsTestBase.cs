using Tiknas.TestBase;

namespace Tiknas.BlobStoring.Aws;

public class TiknasBlobStoringAwsTestCommonBase : TiknasIntegratedTest<TiknasBlobStoringAwsTestCommonModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}

public class TiknasBlobStoringAwsTestBase : TiknasIntegratedTest<TiknasBlobStoringAwsTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
