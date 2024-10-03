using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Tiknas.Aspects;
using Tiknas.AspNetCore.Filters;
using Tiknas.DependencyInjection;
using Tiknas.GlobalFeatures;

namespace Tiknas.AspNetCore.Mvc.GlobalFeatures;

public class GlobalFeatureActionFilter : IAsyncActionFilter, ITiknasFilter, ITransientDependency
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionDescriptor.IsControllerAction())
        {
            await next();
            return;
        }

        if (!GlobalFeatureHelper.IsGlobalFeatureEnabled(context.Controller.GetType(), out var attribute))
        {
            var logger = context.GetService<ILogger<GlobalFeatureActionFilter>>(NullLogger<GlobalFeatureActionFilter>.Instance)!;
            logger.LogWarning($"The '{context.Controller.GetType().FullName}' controller needs to enable '{attribute!.Name}' feature.");
            context.Result = new NotFoundResult();
            return;
        }

        using (TiknasCrossCuttingConcerns.Applying(context.Controller, TiknasCrossCuttingConcerns.GlobalFeatureChecking))
        {
            await next();
        }
    }
}
