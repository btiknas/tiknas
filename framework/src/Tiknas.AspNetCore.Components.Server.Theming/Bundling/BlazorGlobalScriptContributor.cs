using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Components.Web;
using Tiknas.AspNetCore.Mvc.UI.Bundling;

namespace Tiknas.AspNetCore.Components.Server.Theming.Bundling;

public class BlazorGlobalScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<TiknasAspNetCoreComponentsWebOptions>>().Value;
        if (!options.IsBlazorWebApp)
        {
            context.Files.AddIfNotContains("/_framework/blazor.server.js");
        }
        context.Files.AddIfNotContains("/_content/Tiknas.AspNetCore.Components.Web/libs/tiknas/js/tiknas.js");
        context.Files.AddIfNotContains("/_content/Tiknas.AspNetCore.Components.Web/libs/tiknas/js/authentication-state-listener.js");
    }
}
