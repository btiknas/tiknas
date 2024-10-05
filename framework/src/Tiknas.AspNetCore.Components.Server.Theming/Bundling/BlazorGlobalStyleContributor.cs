using System.Collections.Generic;
using Tiknas.AspNetCore.Mvc.UI.Bundling;
using Tiknas.AspNetCore.Mvc.UI.Packages.Bootstrap;
using Tiknas.AspNetCore.Mvc.UI.Packages.FontAwesome;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Components.Server.Theming.Bundling;

[DependsOn(
    typeof(BootstrapStyleContributor),
    typeof(FontAwesomeStyleContributor)
)]
public class BlazorGlobalStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/_content/Tiknas.AspNetCore.Components.Web/libs/tiknas/css/tiknas.css");
        context.Files.AddIfNotContains("/_content/Blazorise/blazorise.css");
        context.Files.AddIfNotContains("/_content/Blazorise.Bootstrap5/blazorise.bootstrap5.css");
        context.Files.AddIfNotContains("/_content/Blazorise.Snackbar/blazorise.snackbar.css");
        context.Files.AddIfNotContains("/_content/Tiknas.BlazoriseUI/tiknas.blazoriseui.css");
    }
}
