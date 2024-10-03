﻿using Tiknas.AspNetCore.Mvc.UI.Bundling;
using Tiknas.AspNetCore.Mvc.UI.Packages.Bootstrap;
using Tiknas.AspNetCore.Mvc.UI.Packages.BootstrapDatepicker;
using Tiknas.AspNetCore.Mvc.UI.Packages.BootstrapDaterangepicker;
using Tiknas.AspNetCore.Mvc.UI.Packages.DatatablesNetBs5;
using Tiknas.AspNetCore.Mvc.UI.Packages.JQuery;
using Tiknas.AspNetCore.Mvc.UI.Packages.JQueryForm;
using Tiknas.AspNetCore.Mvc.UI.Packages.JQueryValidationUnobtrusive;
using Tiknas.AspNetCore.Mvc.UI.Packages.Lodash;
using Tiknas.AspNetCore.Mvc.UI.Packages.Luxon;
using Tiknas.AspNetCore.Mvc.UI.Packages.MalihuCustomScrollbar;
using Tiknas.AspNetCore.Mvc.UI.Packages.Select2;
using Tiknas.AspNetCore.Mvc.UI.Packages.SweetAlert2;
using Tiknas.AspNetCore.Mvc.UI.Packages.Timeago;
using Tiknas.AspNetCore.Mvc.UI.Packages.Toastr;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Bundling;

[DependsOn(
    typeof(JQueryScriptContributor),
    typeof(BootstrapScriptContributor),
    typeof(LodashScriptContributor),
    typeof(JQueryValidationUnobtrusiveScriptContributor),
    typeof(JQueryFormScriptContributor),
    typeof(Select2ScriptContributor),
    typeof(DatatablesNetBs5ScriptContributor),
    typeof(Sweetalert2ScriptContributor),
    typeof(ToastrScriptBundleContributor),
    typeof(MalihuCustomScrollbarPluginScriptBundleContributor),
    typeof(LuxonScriptContributor),
    typeof(TimeagoScriptContributor),
    typeof(BootstrapDatepickerScriptContributor),
    typeof(BootstrapDaterangepickerScriptContributor)
    )]
public class SharedThemeGlobalScriptContributor : BundleContributor
{

    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddRange(new BundleFile[]
        {
            "/libs/tiknas/aspnetcore-mvc-ui-theme-shared/ui-extensions.js",
            "/libs/tiknas/aspnetcore-mvc-ui-theme-shared/jquery/jquery-extensions.js",
            "/libs/tiknas/aspnetcore-mvc-ui-theme-shared/jquery-form/jquery-form-extensions.js",
            "/libs/tiknas/aspnetcore-mvc-ui-theme-shared/jquery/widget-manager.js",
            "/libs/tiknas/aspnetcore-mvc-ui-theme-shared/bootstrap/dom-event-handlers.js",
            "/libs/tiknas/aspnetcore-mvc-ui-theme-shared/bootstrap/modal-manager.js",
            "/libs/tiknas/aspnetcore-mvc-ui-theme-shared/datatables/datatables-extensions.js",
            "/libs/tiknas/aspnetcore-mvc-ui-theme-shared/sweetalert2/tiknas-sweetalert2.js",
            "/libs/tiknas/aspnetcore-mvc-ui-theme-shared/toastr/tiknas-toastr.js",
            "/libs/tiknas/aspnetcore-mvc-ui-theme-shared/date-range-picker/date-range-picker-extensions.js",
            "/libs/tiknas/aspnetcore-mvc-ui-theme-shared/authentication-state/authentication-state-listener.js"
        });
    }
}
