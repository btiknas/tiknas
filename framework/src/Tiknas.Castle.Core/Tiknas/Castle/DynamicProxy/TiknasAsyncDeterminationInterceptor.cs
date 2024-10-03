using Castle.DynamicProxy;
using Tiknas.DynamicProxy;

namespace Tiknas.Castle.DynamicProxy;

public class TiknasAsyncDeterminationInterceptor<TInterceptor> : AsyncDeterminationInterceptor
    where TInterceptor : ITiknasInterceptor
{
    public TiknasAsyncDeterminationInterceptor(TInterceptor tiknasInterceptor)
        : base(new CastleAsyncTiknasInterceptorAdapter<TInterceptor>(tiknasInterceptor))
    {

    }
}
