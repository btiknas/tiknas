using System.Threading.Tasks;

namespace Tiknas.DynamicProxy;

public class AlwaysExceptionAsyncInterceptor : TiknasInterceptor
{
    public override Task InterceptAsync(ITiknasMethodInvocation invocation)
    {
        throw new TiknasException("This interceptor should not be executed!");
    }
}
