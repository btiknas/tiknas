using System.Collections.Concurrent;
using System.Reflection;
using System.Threading.Tasks;

namespace Tiknas.DynamicProxy;

public class SimpleResultCacheTestInterceptor : TiknasInterceptor
{
    private readonly ConcurrentDictionary<MethodInfo, object> _cache;

    public SimpleResultCacheTestInterceptor()
    {
        _cache = new ConcurrentDictionary<MethodInfo, object>();
    }

    public override async Task InterceptAsync(ITiknasMethodInvocation invocation)
    {
        if (_cache.ContainsKey(invocation.Method))
        {
            invocation.ReturnValue = _cache[invocation.Method];
            return;
        }

        await invocation.ProceedAsync();
        _cache[invocation.Method] = invocation.ReturnValue;
    }
}
