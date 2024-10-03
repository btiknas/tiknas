using Tiknas.Http.ProxyScripting.Configuration;
using Tiknas.Http.ProxyScripting.Generators.JQuery;
using Tiknas.Json;
using Tiknas.Minify;
using Tiknas.Modularity;

namespace Tiknas.Http;

[DependsOn(typeof(TiknasHttpAbstractionsModule))]
[DependsOn(typeof(TiknasJsonModule))]
[DependsOn(typeof(TiknasMinifyModule))]
public class TiknasHttpModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasApiProxyScriptingOptions>(options =>
        {
            options.Generators[JQueryProxyScriptGenerator.Name] = typeof(JQueryProxyScriptGenerator);
        });
    }
}
