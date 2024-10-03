using Tiknas.TestBase;

namespace Tiknas.DistributedLocking;

public class TiknasDistributedLockingAbstractionsTestBase : TiknasIntegratedTest<TiknasDistributedLockingAbstractionsTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
