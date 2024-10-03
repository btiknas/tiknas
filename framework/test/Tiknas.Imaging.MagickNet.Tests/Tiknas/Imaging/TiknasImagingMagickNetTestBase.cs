using Tiknas.TestBase;

namespace Tiknas.Imaging;

public abstract class TiknasImagingMagickNetTestBase : TiknasIntegratedTest<TiknasImagingMagickNetTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}