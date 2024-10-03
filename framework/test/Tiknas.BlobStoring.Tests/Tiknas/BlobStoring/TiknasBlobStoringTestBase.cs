using Tiknas.TestBase;

namespace Tiknas.BlobStoring;

public abstract class TiknasBlobStoringTestBase : TiknasIntegratedTest<TiknasBlobStoringTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
