﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Filters;
using Tiknas.DependencyInjection;
using Tiknas.Reflection;
using Tiknas.Validation;

namespace Tiknas.AspNetCore.Mvc.Validation;

public class TiknasValidationActionFilter : IAsyncActionFilter, ITiknasFilter, ITransientDependency
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionDescriptor.IsControllerAction() ||
            !context.ActionDescriptor.HasObjectResult())
        {
            await next();
            return;
        }

        if (!context.GetRequiredService<IOptions<TiknasAspNetCoreMvcOptions>>().Value.AutoModelValidation)
        {
            await next();
            return;
        }

        if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableValidationAttribute>(context.ActionDescriptor.GetMethodInfo()) != null)
        {
            await next();
            return;
        }

        if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableValidationAttribute>(context.Controller.GetType()) != null)
        {
            await next();
            return;
        }

        if (context.ActionDescriptor.GetMethodInfo().DeclaringType != context.Controller.GetType())
        {
            var baseMethod = context.ActionDescriptor.GetMethodInfo();

            var overrideMethod = context.Controller.GetType().GetMethods().FirstOrDefault(x =>
                x.DeclaringType == context.Controller.GetType() &&
                x.Name == baseMethod.Name &&
                x.ReturnType == baseMethod.ReturnType &&
                x.GetParameters().Select(p => p.ToString()).SequenceEqual(baseMethod.GetParameters().Select(p => p.ToString())));

            if (overrideMethod != null)
            {
                if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableValidationAttribute>(overrideMethod) != null)
                {
                    await next();
                    return;
                }
            }
        }

        context.GetRequiredService<IModelStateValidator>().Validate(context.ModelState);
        await next();
    }
}
