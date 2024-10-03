using System.Threading.Tasks;
using Tiknas.DependencyInjection;

namespace Tiknas.EventBus.Local;

public class MySimpleEventDataHandler : ILocalEventHandler<MySimpleEventData>, ISingletonDependency
{
    public int TotalData { get; private set; }

    public Task HandleEventAsync(MySimpleEventData eventData)
    {
        TotalData += eventData.Value;
        return Task.CompletedTask;
    }
}
