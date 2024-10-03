using Tiknas.TestBase;

namespace Tiknas.MemoryDb;

public abstract class MemoryDbTestBase : TiknasIntegratedTest<TiknasMemoryDbTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
