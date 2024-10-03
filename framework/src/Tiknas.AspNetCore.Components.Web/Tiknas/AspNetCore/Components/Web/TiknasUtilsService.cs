using System.Threading.Tasks;
using Microsoft.JSInterop;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.Web;

public class TiknasUtilsService : ITiknasUtilsService, ITransientDependency
{
    protected IJSRuntime JsRuntime { get; }

    public TiknasUtilsService(IJSRuntime jsRuntime)
    {
        JsRuntime = jsRuntime;
    }

    public ValueTask AddClassToTagAsync(string tagName, string className)
    {
        return JsRuntime.InvokeVoidAsync("tiknas.utils.addClassToTag", tagName, className);
    }

    public ValueTask RemoveClassFromTagAsync(string tagName, string className)
    {
        return JsRuntime.InvokeVoidAsync("tiknas.utils.removeClassFromTag", tagName, className);
    }

    public ValueTask<bool> HasClassOnTagAsync(string tagName, string className)
    {
        return JsRuntime.InvokeAsync<bool>("tiknas.utils.hasClassOnTag", tagName, className);
    }

    public ValueTask ReplaceLinkHrefByIdAsync(string linkId, string hrefValue)
    {
        return JsRuntime.InvokeVoidAsync("tiknas.utils.replaceLinkHrefById", linkId, hrefValue);
    }

    public ValueTask ToggleFullscreenAsync()
    {
        return JsRuntime.InvokeVoidAsync("tiknas.utils.toggleFullscreen");
    }

    public ValueTask RequestFullscreenAsync()
    {
        return JsRuntime.InvokeVoidAsync("tiknas.utils.requestFullscreen");
    }

    public ValueTask ExitFullscreenAsync()
    {
        return JsRuntime.InvokeVoidAsync("tiknas.utils.exitFullscreen");
    }
}
