using Tiknas.TestBase;

namespace Tiknas.Dapper;

public abstract class DapperTestBase : TiknasIntegratedTest<TiknasDapperTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
