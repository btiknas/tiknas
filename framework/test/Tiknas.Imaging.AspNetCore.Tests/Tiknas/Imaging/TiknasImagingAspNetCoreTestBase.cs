using Tiknas.TestBase;

namespace Tiknas.Imaging;

public abstract class TiknasImagingAspNetCoreTestBase : TiknasIntegratedTest<TiknasImagingAspNetCoreTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}