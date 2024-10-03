using System.Linq;
using System.Reflection;
using Tiknas.Modularity;

namespace Tiknas.AspNetCore.Components.WebAssembly.WebApp;

public static class WebAppAdditionalAssembliesHelper
{
    public static Assembly[] GetAssemblies<TModule>()
        where TModule : ITiknasModule
    {
        return TiknasModuleHelper.FindAllModuleTypes(typeof(TModule), null)
            .Where(t => t.Name.Contains("Blazor") || t.Name.Contains("WebAssembly"))
            .Select(t => t.Assembly)
            .Distinct()
            .ToArray();
    }
}
