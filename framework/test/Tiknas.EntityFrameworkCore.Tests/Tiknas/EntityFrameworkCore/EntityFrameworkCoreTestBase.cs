using Tiknas.TestApp.Testing;

namespace Tiknas.EntityFrameworkCore;

public abstract class EntityFrameworkCoreTestBase : TestAppTestBase<TiknasEntityFrameworkCoreTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
