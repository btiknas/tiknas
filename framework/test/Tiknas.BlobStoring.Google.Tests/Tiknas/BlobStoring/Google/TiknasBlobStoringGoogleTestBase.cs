using Tiknas.TestBase;

namespace Tiknas.BlobStoring.Google;

public class TiknasBlobStoringGoogleTestCommonBase : TiknasIntegratedTest<TiknasBlobStoringGoogleTestCommonModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}

public class TiknasBlobStoringGoogleTestBase : TiknasIntegratedTest<TiknasBlobStoringGoogleTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
