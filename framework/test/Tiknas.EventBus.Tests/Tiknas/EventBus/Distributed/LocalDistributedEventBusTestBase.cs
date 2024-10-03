using Tiknas.TestBase;

namespace Tiknas.EventBus.Distributed;

public abstract class LocalDistributedEventBusTestBase : TiknasIntegratedTest<EventBusTestModule>
{
    protected IDistributedEventBus DistributedEventBus;

    protected LocalDistributedEventBusTestBase()
    {
        DistributedEventBus = GetRequiredService<LocalDistributedEventBus>();
    }

    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
