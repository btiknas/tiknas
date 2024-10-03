using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Tiknas.DynamicProxy;

namespace Tiknas.Castle.DynamicProxy;

public class CastleTiknasMethodInvocationAdapter : CastleTiknasMethodInvocationAdapterBase, ITiknasMethodInvocation
{
    protected IInvocationProceedInfo ProceedInfo { get; }
    protected Func<IInvocation, IInvocationProceedInfo, Task> Proceed { get; }

    public CastleTiknasMethodInvocationAdapter(IInvocation invocation, IInvocationProceedInfo proceedInfo,
        Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        : base(invocation)
    {
        ProceedInfo = proceedInfo;
        Proceed = proceed;
    }

    public override async Task ProceedAsync()
    {
        await Proceed(Invocation, ProceedInfo);
    }
}
