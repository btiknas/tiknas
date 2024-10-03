using Tiknas.TestBase;

namespace Tiknas.IdentityModel;

public abstract class TiknasIdentityModelTestBase : TiknasIntegratedTest<TiknasIdentityModelTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
