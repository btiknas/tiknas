using Tiknas.TestBase;

namespace Tiknas.TestApp;

public class TestAppTestBase : TiknasIntegratedTest<TestAppTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
