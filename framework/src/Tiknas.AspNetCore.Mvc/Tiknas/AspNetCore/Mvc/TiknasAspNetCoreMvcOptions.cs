using System;
using System.Collections.Generic;
using Tiknas.AspNetCore.Mvc.Conventions;

namespace Tiknas.AspNetCore.Mvc;

public class TiknasAspNetCoreMvcOptions
{
    public bool? MinifyGeneratedScript { get; set; }

    public TiknasConventionalControllerOptions ConventionalControllers { get; }

    public HashSet<Type> IgnoredControllersOnModelExclusion { get; }

    public HashSet<Type> ControllersToRemove { get; }

    public bool ExposeIntegrationServices { get; set; } = false;

    public bool AutoModelValidation { get; set; }

    public bool EnableRazorRuntimeCompilationOnDevelopment { get; set; }

    public bool ChangeControllerModelApiExplorerGroupName { get; set; }

    public TiknasAspNetCoreMvcOptions()
    {
        ConventionalControllers = new TiknasConventionalControllerOptions();
        IgnoredControllersOnModelExclusion = new HashSet<Type>();
        ControllersToRemove = new HashSet<Type>();
        AutoModelValidation = true;
        EnableRazorRuntimeCompilationOnDevelopment = true;
        ChangeControllerModelApiExplorerGroupName = true;
    }
}
