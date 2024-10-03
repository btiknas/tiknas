using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tiknas;
using Tiknas.AspNetCore;
using Tiknas.AspNetCore.Mvc;
using Tiknas.AspNetCore.TestBase;
using Tiknas.Modularity.PlugIns;

var builder = WebApplication.CreateBuilder();
await builder.RunTiknasModuleAsync<TiknasAspNetCoreMvcTestModule>(options =>
{
    var hostEnvironment = options.Services.GetHostingEnvironment();
    var currentDirectory = hostEnvironment.ContentRootPath;
    var plugDllInPath = "";

    for (var i = 0; i < 10; i++)
    {
        var parentDirectory = new DirectoryInfo(currentDirectory).Parent;
        if (parentDirectory == null)
        {
            break;
        }

        if (parentDirectory.Name == "test")
        {
#if DEBUG
            plugDllInPath = Path.Combine(parentDirectory.FullName, "Tiknas.AspNetCore.Mvc.PlugIn", "bin", "Debug", "net9.0");
#else
            plugDllInPath = Path.Combine(parentDirectory.FullName, "Tiknas.AspNetCore.Mvc.PlugIn", "bin", "Release", "net9.0");
#endif
            break;
        }

        currentDirectory = parentDirectory.FullName;
    }

    if (plugDllInPath.IsNullOrWhiteSpace())
    {
        throw new TiknasException("Could not find the plug DLL path!");
    }

    options.PlugInSources.AddFolder(plugDllInPath);
});

public partial class Program
{
}
