using System.Threading.Tasks;
using Tiknas.TestBase.Logging;

namespace Tiknas.DynamicProxy;

public class SimpleAsyncInterceptor : TiknasInterceptor
{
    public override async Task InterceptAsync(ITiknasMethodInvocation invocation)
    {
        await Task.Delay(5);
        (invocation.TargetObject as ICanLogOnObject)?.Logs?.Add($"{GetType().Name}_InterceptAsync_BeforeInvocation");
        await invocation.ProceedAsync();
        (invocation.TargetObject as ICanLogOnObject)?.Logs?.Add($"{GetType().Name}_InterceptAsync_AfterInvocation");
        await Task.Delay(5);
    }
}
