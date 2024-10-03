using Tiknas.AspNetCore.Mvc.UI.Bundling;
using Tiknas.AspNetCore.Mvc.UI.Packages.Bootstrap;
using Tiknas.AspNetCore.Mvc.UI.Packages.BootstrapDatepicker;
using Tiknas.AspNetCore.Mvc.UI.Packages.BootstrapDaterangepicker;
using Tiknas.AspNetCore.Mvc.UI.Packages.Core;
using Tiknas.AspNetCore.Mvc.UI.Packages.DatatablesNetBs5;
using Tiknas.AspNetCore.Mvc.UI.Packages.FontAwesome;
using Tiknas.AspNetCore.Mvc.UI.Packages.MalihuCustomScrollbar;
using Tiknas.AspNetCore.Mvc.UI.Packages.Select2;
using Tiknas.AspNetCore.Mvc.UI.Packages.Toastr;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Bundling;

[DependsOn(
    typeof(CoreStyleContributor),
    typeof(BootstrapStyleContributor),
    typeof(FontAwesomeStyleContributor),
    typeof(ToastrStyleBundleContributor),
    typeof(Select2StyleContributor),
    typeof(MalihuCustomScrollbarPluginStyleBundleContributor),
    typeof(DatatablesNetBs5StyleContributor),
    typeof(BootstrapDatepickerStyleContributor),
    typeof(BootstrapDaterangepickerStyleContributor)
)]
public class SharedThemeGlobalStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddRange(new BundleFile[]
        {
            "/libs/tiknas/aspnetcore-mvc-ui-theme-shared/datatables/datatables-styles.css",
            "/libs/tiknas/aspnetcore-mvc-ui-theme-shared/date-range-picker/date-range-picker-styles.css"
        });
    }
}
