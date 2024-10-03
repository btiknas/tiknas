using System.Reflection;

namespace Tiknas.AspNetCore.Components.Web.Theming.Routing;

public class TiknasRouterOptions
{
    public Assembly AppAssembly { get; set; } = default!;

    public RouterAssemblyList AdditionalAssemblies { get; }

    public TiknasRouterOptions()
    {
        AdditionalAssemblies = new RouterAssemblyList();
    }
}
