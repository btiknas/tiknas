using Tiknas.Modularity;

namespace Tiknas.TextTemplating.Scriban;

[DependsOn(
    typeof(TiknasTextTemplatingCoreModule)
)]
public class TiknasTextTemplatingScribanModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasTextTemplatingOptions>(options =>
        {
            options.DefaultRenderingEngine = ScribanTemplateRenderingEngine.EngineName;
            options.RenderingEngines[ScribanTemplateRenderingEngine.EngineName] = typeof(ScribanTemplateRenderingEngine);
        });
    }
}
