using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Shouldly;
using Tiknas.DependencyInjection;
using Tiknas.EventBus.Distributed;
using Tiknas.EventBus.Local;
using Xunit;

namespace Tiknas.EventBus;

public class LocalAndDistributeEventHandlerRegister_Tests : EventBusTestBase
{
    [Fact]
    public void Should_Register_Both_Local_And_Distribute()
    {
        var localOptions = GetRequiredService<IOptions<TiknasLocalEventBusOptions>>();
        var distributedOptions = GetRequiredService<IOptions<TiknasDistributedEventBusOptions>>();

        localOptions.Value.Handlers.ShouldContain(x => x == typeof(MyEventHandle));
        distributedOptions.Value.Handlers.ShouldContain(x => x == typeof(MyEventHandle));
    }

    class MyEventDate
    {

    }

    class MyEventHandle : ILocalEventHandler<MyEventDate>, IDistributedEventHandler<MyEventDate>, ITransientDependency
    {
        Task ILocalEventHandler<MyEventDate>.HandleEventAsync(MyEventDate eventData)
        {
            return Task.CompletedTask;
        }

        Task IDistributedEventHandler<MyEventDate>.HandleEventAsync(MyEventDate eventData)
        {
            return Task.CompletedTask;
        }
    }
}
