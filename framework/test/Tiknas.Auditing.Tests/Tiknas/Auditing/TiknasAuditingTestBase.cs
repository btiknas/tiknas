using Tiknas.TestBase;

namespace Tiknas.Auditing;

public class TiknasAuditingTestBase : TiknasIntegratedTest<TiknasAuditingTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
