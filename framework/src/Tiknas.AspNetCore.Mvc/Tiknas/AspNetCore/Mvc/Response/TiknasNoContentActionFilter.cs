using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Tiknas.AspNetCore.Filters;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Mvc.Response;

public class TiknasNoContentActionFilter : IAsyncActionFilter, ITiknasFilter, ITransientDependency
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionDescriptor.IsControllerAction())
        {
            await next();
            return;
        }

        await next();

        if (!context.HttpContext.Response.HasStarted &&
            context.HttpContext.Response.StatusCode == (int)HttpStatusCode.OK &&
            context.Result == null)
        {
            var returnType = context.ActionDescriptor.GetReturnType();
            if (returnType == typeof(Task) || returnType == typeof(void))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }
        }
    }
}
