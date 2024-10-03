using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Tiknas.AspNetCore.VirtualFileSystem;
using Tiknas.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class TiknasMvcBuilderExtensions
{
    public static void AddApplicationPartIfNotExists(this IMvcBuilder mvcBuilder, Assembly assembly)
    {
        mvcBuilder.PartManager.ApplicationParts.AddIfNotContains(assembly);
    }

    public static void AddApplicationPartIfNotExists(this IMvcCoreBuilder mvcCoreBuilder, Assembly assembly)
    {
        mvcCoreBuilder.PartManager.ApplicationParts.AddIfNotContains(assembly);
    }

    public static void AddIfNotContains(this IList<ApplicationPart> applicationParts, Assembly assembly)
    {
        if (applicationParts.Any(
            p => p is AssemblyPart assemblyPart && assemblyPart.Assembly == assembly))
        {
            return;
        }

        applicationParts.Add(new AssemblyPart(assembly));
    }

    public static void AddTiknasRazorRuntimeCompilation(this IMvcCoreBuilder mvcCoreBuilder)
    {
        mvcCoreBuilder.AddRazorRuntimeCompilation();
        mvcCoreBuilder.Services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
        {
            options.FileProviders.Add(
                new RazorViewEngineVirtualFileProvider(
                    mvcCoreBuilder.Services.GetSingletonInstance<IObjectAccessor<IServiceProvider>>()
                )
            );
        });
    }
}
