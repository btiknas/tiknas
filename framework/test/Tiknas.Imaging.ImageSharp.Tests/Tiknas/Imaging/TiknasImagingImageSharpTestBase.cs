using Tiknas.TestBase;

namespace Tiknas.Imaging;

public abstract class TiknasImagingImageSharpTestBase : TiknasIntegratedTest<TiknasImagingImageSharpTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}