using System.Threading.Tasks;
using Microsoft.JSInterop;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.Web;

[Dependency(ReplaceServices = true)]
public class CookieService : ICookieService, ITransientDependency
{
    public IJSRuntime JsRuntime { get; }

    public CookieService(IJSRuntime jsRuntime)
    {
        JsRuntime = jsRuntime;
    }

    public async ValueTask SetAsync(string key, string value, CookieOptions? options)
    {
        await JsRuntime.InvokeVoidAsync("tiknas.utils.setCookieValue", key, value, options?.ExpireDate?.ToString("r"), options?.Path, options?.Secure);
    }

    public async ValueTask<string> GetAsync(string key)
    {
        return await JsRuntime.InvokeAsync<string>("tiknas.utils.getCookieValue", key);
    }

    public async ValueTask DeleteAsync(string key, string? path = null)
    {
        await JsRuntime.InvokeVoidAsync("tiknas.utils.deleteCookie", key);
    }
}
