using System.Threading.Tasks;
using Tiknas.DependencyInjection;
using Tiknas.Domain.Repositories;
using Tiknas.DynamicProxy;

namespace Tiknas.Domain.ChangeTracking;

public class ChangeTrackingInterceptor : TiknasInterceptor, ITransientDependency
{
    private readonly IEntityChangeTrackingProvider _entityChangeTrackingProvider;

    public ChangeTrackingInterceptor(IEntityChangeTrackingProvider entityChangeTrackingProvider)
    {
        _entityChangeTrackingProvider = entityChangeTrackingProvider;
    }

    public async override Task InterceptAsync(ITiknasMethodInvocation invocation)
    {
        if (!ChangeTrackingHelper.IsEntityChangeTrackingMethod(invocation.Method, out var changeTrackingAttribute))
        {
            await invocation.ProceedAsync();
            return;
        }

        using (_entityChangeTrackingProvider.Change(changeTrackingAttribute?.IsEnabled))
        {
            await invocation.ProceedAsync();
        }
    }
}
