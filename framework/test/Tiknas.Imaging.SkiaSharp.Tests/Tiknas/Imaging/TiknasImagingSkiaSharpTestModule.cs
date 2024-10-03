using Tiknas.TestBase;

namespace Tiknas.Imaging;

public abstract class TiknasImagingSkiaSharpTestBase : TiknasIntegratedTest<TiknasImagingSkiaSharpTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
