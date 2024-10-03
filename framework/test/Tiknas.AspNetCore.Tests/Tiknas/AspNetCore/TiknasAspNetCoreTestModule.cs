using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.TestBase;
using Tiknas.Autofac;
using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.VirtualFileSystem;

namespace Tiknas.AspNetCore;

[DependsOn(
    typeof(TiknasAspNetCoreTestBaseModule),
    typeof(TiknasAspNetCoreModule),
    typeof(TiknasAutofacModule)
    )]
public class TiknasAspNetCoreTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasAspNetCoreTestModule>();
                //options.FileSets.ReplaceEmbeddedByPhysical<TiknasAspNetCoreTestModule>(FindProjectPath(hostingEnvironment));
            });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseCorrelationId();
        app.UseStaticFiles();
    }

    private string FindProjectPath(IWebHostEnvironment hostEnvironment)
    {
        var directory = new DirectoryInfo(hostEnvironment.ContentRootPath);

        while (directory != null && directory.Name != "Tiknas.AspNetCore.Tests")
        {
            directory = directory.Parent;
        }

        return directory?.FullName
               ?? throw new Exception("Could not find the project path by beginning from " + hostEnvironment.ContentRootPath + ", going through to parents and looking for Tiknas.AspNetCore.Tests");
    }
}
