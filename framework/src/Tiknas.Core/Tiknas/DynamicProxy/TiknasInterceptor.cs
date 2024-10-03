using System.Threading.Tasks;

namespace Tiknas.DynamicProxy;

public abstract class TiknasInterceptor : ITiknasInterceptor
{
    public abstract Task InterceptAsync(ITiknasMethodInvocation invocation);
}
