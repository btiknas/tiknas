using Tiknas.AspNetCore.Mvc.Libs;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap;
using Tiknas.Data;
using Tiknas.Minify;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Mvc.UI.Bundling;

[DependsOn(
    typeof(TiknasAspNetCoreMvcUiBootstrapModule),
    typeof(TiknasMinifyModule),
    typeof(TiknasAspNetCoreMvcUiBundlingAbstractionsModule)
    )]
public class TiknasAspNetCoreMvcUiBundlingModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        if (!context.Services.IsDataMigrationEnvironment())
        {
            Configure<TiknasMvcLibsOptions>(options =>
            {
                options.CheckLibs = true;
            });
        }
    }
}
