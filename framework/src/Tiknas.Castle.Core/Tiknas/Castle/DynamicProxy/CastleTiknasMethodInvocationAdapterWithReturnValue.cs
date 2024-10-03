using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Tiknas.DynamicProxy;

namespace Tiknas.Castle.DynamicProxy;

public class CastleTiknasMethodInvocationAdapterWithReturnValue<TResult> : CastleTiknasMethodInvocationAdapterBase, ITiknasMethodInvocation
{
    protected IInvocationProceedInfo ProceedInfo { get; }
    protected Func<IInvocation, IInvocationProceedInfo, Task<TResult>> Proceed { get; }

    public CastleTiknasMethodInvocationAdapterWithReturnValue(IInvocation invocation,
        IInvocationProceedInfo proceedInfo,
        Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        : base(invocation)
    {
        ProceedInfo = proceedInfo;
        Proceed = proceed;
    }

    public override async Task ProceedAsync()
    {
        ReturnValue = (await Proceed(Invocation, ProceedInfo))!;
    }
}
