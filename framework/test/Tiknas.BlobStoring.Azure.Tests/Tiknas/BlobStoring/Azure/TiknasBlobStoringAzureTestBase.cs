using Tiknas.TestBase;

namespace Tiknas.BlobStoring.Azure;

public class TiknasBlobStoringAzureTestCommonBase : TiknasIntegratedTest<TiknasBlobStoringAzureTestCommonModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}

public class TiknasBlobStoringAzureTestBase : TiknasIntegratedTest<TiknasBlobStoringAzureTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
