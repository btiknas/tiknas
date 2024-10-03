using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Tiknas.AspNetCore.Filters;

namespace Tiknas.AspNetCore.Mvc.ApplicationModels;

public class TiknasMvcActionDescriptorProvider : IActionDescriptorProvider
{
    public virtual int Order => -1000 + 10;

    public virtual void OnProvidersExecuting(ActionDescriptorProviderContext context)
    {
    }

    public virtual void OnProvidersExecuted(ActionDescriptorProviderContext context)
    {
        foreach (var action in context.Results.Where(x => x is ControllerActionDescriptor).Cast<ControllerActionDescriptor>())
        {
            var disableTiknasFeaturesAttribute = action.ControllerTypeInfo.GetCustomAttribute<DisableTiknasFeaturesAttribute>(true);
            if (disableTiknasFeaturesAttribute != null && disableTiknasFeaturesAttribute.DisableMvcFilters)
            {
                action.FilterDescriptors.RemoveAll(x => x.Filter is ServiceFilterAttribute serviceFilterAttribute &&
                                                        typeof(ITiknasFilter).IsAssignableFrom(serviceFilterAttribute.ServiceType));
            }
        }
    }
}
