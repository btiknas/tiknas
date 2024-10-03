using Tiknas.TestBase;

namespace Tiknas.Features;

public class FeatureTestBase : TiknasIntegratedTest<TiknasFeaturesTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
