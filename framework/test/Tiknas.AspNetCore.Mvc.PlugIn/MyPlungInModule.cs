using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.AspNetCore.Mvc.UI.Theme.Shared;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Mvc.PlugIn;

[DependsOn(typeof(TiknasAspNetCoreMvcUiThemeSharedModule))]
public class MyPlungInModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            //Add plugin assembly
            mvcBuilder.PartManager.ApplicationParts.Add(new AssemblyPart(typeof(MyPlungInModule).Assembly));

            //Add CompiledRazorAssemblyPart if the PlugIn module contains razor views.
            mvcBuilder.PartManager.ApplicationParts.Add(new CompiledRazorAssemblyPart(typeof(MyPlungInModule).Assembly));
        });
    }
}
