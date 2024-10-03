using Tiknas.TestBase;

namespace Tiknas.EventBus.Local;

public abstract class EventBusTestBase : TiknasIntegratedTest<EventBusTestModule>
{
    protected ILocalEventBus LocalEventBus;

    protected EventBusTestBase()
    {
        LocalEventBus = GetRequiredService<ILocalEventBus>();
    }

    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
