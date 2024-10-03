using System.Threading.Tasks;
using Microsoft.JSInterop;
using Tiknas.AspNetCore.Components.BlockUi;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.Web.BlockUi;

[Dependency(ReplaceServices = true)]
public class TiknasBlockUiService : IBlockUiService, IScopedDependency
{
    public IJSRuntime JsRuntime { get; }

    public TiknasBlockUiService(IJSRuntime jsRuntime)
    {
        JsRuntime = jsRuntime;
    }

    public async Task Block(string? selectors, bool busy = false)
    {
        await JsRuntime.InvokeVoidAsync("tiknas.ui.block", selectors, busy);
    }

    public async Task UnBlock()
    {
        await JsRuntime.InvokeVoidAsync("tiknas.ui.unblock");
    }
}
