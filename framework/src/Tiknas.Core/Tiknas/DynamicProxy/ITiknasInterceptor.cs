using System.Threading.Tasks;

namespace Tiknas.DynamicProxy;

public interface ITiknasInterceptor
{
    Task InterceptAsync(ITiknasMethodInvocation invocation);
}
