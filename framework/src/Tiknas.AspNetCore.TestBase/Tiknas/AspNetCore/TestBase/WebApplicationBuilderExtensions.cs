using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.TestBase;

public static class WebApplicationBuilderExtensions
{
    public async static Task RunTiknasModuleAsync<TModule>(this WebApplicationBuilder builder,  Action<TiknasApplicationCreationOptions>? optionsAction = null, string? applicationName = null)
        where TModule : ITiknasModule
    {
        applicationName = applicationName ?? typeof(TModule).Assembly.GetName()?.Name;
        if (!applicationName.IsNullOrWhiteSpace())
        {
            // Set the application name as the assembly name of the module will automatically add assembly to the ApplicationParts of MVC application.
            builder.Environment.ApplicationName = applicationName;
        }

        builder.Host.UseAutofac();
        await builder.AddApplicationAsync<TModule>(optionsAction);
        var app = builder.Build();
        await app.InitializeApplicationAsync();
        await app.RunAsync();
    }
}
