using Tiknas.TestBase;

namespace Tiknas.GlobalFeatures;

public abstract class GlobalFeatureTestBase : TiknasIntegratedTest<GlobalFeatureTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
