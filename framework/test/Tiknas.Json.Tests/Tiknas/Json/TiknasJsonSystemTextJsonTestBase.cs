using Tiknas.TestBase;

namespace Tiknas.Json;

public abstract class TiknasJsonSystemTextJsonTestBase : TiknasIntegratedTest<TiknasJsonSystemTextJsonTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}

public abstract class TiknasJsonNewtonsoftJsonTestBase : TiknasIntegratedTest<TiknasJsonNewtonsoftTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
