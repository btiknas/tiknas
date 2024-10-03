using Tiknas.TestBase;

namespace Tiknas.ExceptionHandling;

public class TiknasExceptionHandlingTestBase : TiknasIntegratedTest<TiknasExceptionHandlingTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
