using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap;
using Tiknas.AspNetCore.Mvc.UI.Bundling;
using Tiknas.Modularity;
using Tiknas.VirtualFileSystem;

namespace Tiknas.AspNetCore.Mvc.UI.Widgets;

[DependsOn(
    typeof(TiknasAspNetCoreMvcUiBootstrapModule),
    typeof(TiknasAspNetCoreMvcUiBundlingModule)
)]
public class TiknasAspNetCoreMvcUiWidgetsModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(TiknasAspNetCoreMvcUiWidgetsModule).Assembly);
        });

        AutoAddWidgets(context.Services);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<DefaultViewComponentHelper>();

        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasAspNetCoreMvcUiWidgetsModule>();
        });
    }

    private static void AutoAddWidgets(IServiceCollection services)
    {
        var widgetTypes = new List<Type>();

        services.OnRegistered(context =>
        {
            if (WidgetAttribute.IsWidget(context.ImplementationType))
            {
                widgetTypes.Add(context.ImplementationType);
            }
        });

        services.Configure<TiknasWidgetOptions>(options =>
        {
            foreach (var widgetType in widgetTypes)
            {
                options.Widgets.Add(new WidgetDefinition(widgetType));
            }
        });
    }
}
