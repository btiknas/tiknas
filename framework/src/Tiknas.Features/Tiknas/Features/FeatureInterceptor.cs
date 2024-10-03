using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Aspects;
using Tiknas.DependencyInjection;
using Tiknas.DynamicProxy;

namespace Tiknas.Features;

public class FeatureInterceptor : TiknasInterceptor, ITransientDependency
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public FeatureInterceptor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task InterceptAsync(ITiknasMethodInvocation invocation)
    {
        if (TiknasCrossCuttingConcerns.IsApplied(invocation.TargetObject, TiknasCrossCuttingConcerns.FeatureChecking))
        {
            await invocation.ProceedAsync();
            return;
        }

        await CheckFeaturesAsync(invocation);
        await invocation.ProceedAsync();
    }

    protected virtual async Task CheckFeaturesAsync(ITiknasMethodInvocation invocation)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            await scope.ServiceProvider.GetRequiredService<IMethodInvocationFeatureCheckerService>().CheckAsync(
                new MethodInvocationFeatureCheckerContext(
                    invocation.Method
                )
            );
        }
    }
}
