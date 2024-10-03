using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Tiknas.Aspects;
using Tiknas.AspNetCore.Filters;
using Tiknas.DependencyInjection;
using Tiknas.Features;

namespace Tiknas.AspNetCore.Mvc.Features;

public class TiknasFeaturePageFilter : IAsyncPageFilter, ITiknasFilter, ITransientDependency
{
    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        if (context.HandlerMethod == null || !context.ActionDescriptor.IsPageAction())
        {
            await next();
            return;
        }

        var methodInfo = context.HandlerMethod.MethodInfo;

        using (TiknasCrossCuttingConcerns.Applying(context.HandlerInstance, TiknasCrossCuttingConcerns.FeatureChecking))
        {
            var methodInvocationFeatureCheckerService = context.GetRequiredService<IMethodInvocationFeatureCheckerService>();
            await methodInvocationFeatureCheckerService.CheckAsync(new MethodInvocationFeatureCheckerContext(methodInfo));

            await next();
        }
    }
}
