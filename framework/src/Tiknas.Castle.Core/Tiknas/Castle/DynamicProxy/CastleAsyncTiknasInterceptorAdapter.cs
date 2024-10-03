using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Tiknas.DynamicProxy;

namespace Tiknas.Castle.DynamicProxy;

public class CastleAsyncTiknasInterceptorAdapter<TInterceptor> : AsyncInterceptorBase
    where TInterceptor : ITiknasInterceptor
{
    private readonly TInterceptor _tiknasInterceptor;

    public CastleAsyncTiknasInterceptorAdapter(TInterceptor tiknasInterceptor)
    {
        _tiknasInterceptor = tiknasInterceptor;
    }

    protected override async Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
    {
        await _tiknasInterceptor.InterceptAsync(
            new CastleTiknasMethodInvocationAdapter(invocation, proceedInfo, proceed)
        );
    }

    protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
    {
        var adapter = new CastleTiknasMethodInvocationAdapterWithReturnValue<TResult>(invocation, proceedInfo, proceed);

        await _tiknasInterceptor.InterceptAsync(
            adapter
        );

        return (TResult)adapter.ReturnValue;
    }
}
