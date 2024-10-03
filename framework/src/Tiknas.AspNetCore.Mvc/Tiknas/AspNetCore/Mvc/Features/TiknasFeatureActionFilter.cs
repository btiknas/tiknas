using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Tiknas.Aspects;
using Tiknas.AspNetCore.Filters;
using Tiknas.DependencyInjection;
using Tiknas.Features;

namespace Tiknas.AspNetCore.Mvc.Features;

public class TiknasFeatureActionFilter : IAsyncActionFilter, ITiknasFilter, ITransientDependency
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionDescriptor.IsControllerAction())
        {
            await next();
            return;
        }

        var methodInfo = context.ActionDescriptor.GetMethodInfo();

        using (TiknasCrossCuttingConcerns.Applying(context.Controller, TiknasCrossCuttingConcerns.FeatureChecking))
        {
            var methodInvocationFeatureCheckerService = context.GetRequiredService<IMethodInvocationFeatureCheckerService>();
            await methodInvocationFeatureCheckerService.CheckAsync(new MethodInvocationFeatureCheckerContext(methodInfo));

            await next();
        }
    }
}
