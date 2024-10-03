using Tiknas.TestBase;

namespace Tiknas.Imaging;

public abstract class TiknasImagingAbstractionsTestBase : TiknasIntegratedTest<TiknasImagingAbstractionsTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}