using System.Threading.Tasks;
using Tiknas.Aspects;
using Tiknas.DependencyInjection;
using Tiknas.DynamicProxy;

namespace Tiknas.GlobalFeatures;

public class GlobalFeatureInterceptor : TiknasInterceptor, ITransientDependency
{
    public override async Task InterceptAsync(ITiknasMethodInvocation invocation)
    {
        if (TiknasCrossCuttingConcerns.IsApplied(invocation.TargetObject, TiknasCrossCuttingConcerns.GlobalFeatureChecking))
        {
            await invocation.ProceedAsync();
            return;
        }

        if (!GlobalFeatureHelper.IsGlobalFeatureEnabled(invocation.TargetObject.GetType(), out var attribute))
        {
            throw new TiknasGlobalFeatureNotEnabledException(code: TiknasGlobalFeatureErrorCodes.GlobalFeatureIsNotEnabled)
                .WithData("ServiceName", invocation.TargetObject.GetType().FullName!)
                .WithData("GlobalFeatureName", attribute!.Name!);
        }

        await invocation.ProceedAsync();
    }
}
