using System;
using Tiknas.Modularity;

namespace Tiknas.TextTemplating.Razor;

[DependsOn(
    typeof(TiknasTextTemplatingCoreModule)
)]
public class TiknasTextTemplatingRazorModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasTextTemplatingOptions>(options =>
        {
            if (options.DefaultRenderingEngine.IsNullOrWhiteSpace())
            {
                options.DefaultRenderingEngine = RazorTemplateRenderingEngine.EngineName;
            }
            options.RenderingEngines[RazorTemplateRenderingEngine.EngineName] = typeof(RazorTemplateRenderingEngine);
        });
    }
}
