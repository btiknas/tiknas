using Tiknas.TestBase;

namespace Tiknas.AspNetCore.SignalR;

public abstract class TiknasAspNetCoreSignalRTestBase : TiknasIntegratedTest<TiknasAspNetCoreSignalRTestModule>
{
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
