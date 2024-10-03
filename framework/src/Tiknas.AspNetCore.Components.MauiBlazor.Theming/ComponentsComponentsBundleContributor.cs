using Tiknas.Bundling;

namespace Tiknas.AspNetCore.Components.MauiBlazor.Theming;

public class ComponentsComponentsBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {
        context.Add("_content/Tiknas.AspNetCore.Components.Web/libs/tiknas/js/tiknas.js");
        context.Add("_content/Tiknas.AspNetCore.Components.Web/libs/tiknas/js/lang-utils.js");
    }

    public void AddStyles(BundleContext context)
    {
        if (!context.InteractiveAuto)
        {
            context.BundleDefinitions.Insert(0, new BundleDefinition
            {
                Source = "_content/Tiknas.AspNetCore.Components.MauiBlazor.Theming/libs/bootstrap/css/bootstrap.min.css"
            });
            context.BundleDefinitions.Insert(1, new BundleDefinition
            {
                Source = "_content/Tiknas.AspNetCore.Components.MauiBlazor.Theming/libs/fontawesome/css/all.css"
            });
        }
        context.Add("_content/Tiknas.AspNetCore.Components.Web/libs/tiknas/css/tiknas.css");
        context.Add("_content/Tiknas.AspNetCore.Components.MauiBlazor.Theming/libs/flag-icon/css/flag-icon.css");
        context.Add("_content/Blazorise/blazorise.css");
        context.Add("_content/Blazorise.Bootstrap5/blazorise.bootstrap5.css");
        context.Add("_content/Blazorise.Snackbar/blazorise.snackbar.css");
    }
}
