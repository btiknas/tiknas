using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Mvc.AntiForgery;

public class AspNetCoreTiknasAntiForgeryManager : ITiknasAntiForgeryManager, ITransientDependency
{
    protected TiknasAntiForgeryOptions Options { get; }

    protected HttpContext HttpContext => _httpContextAccessor.HttpContext!;

    private readonly IAntiforgery _antiforgery;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AspNetCoreTiknasAntiForgeryManager(
        IAntiforgery antiforgery,
        IHttpContextAccessor httpContextAccessor,
        IOptions<TiknasAntiForgeryOptions> options)
    {
        _antiforgery = antiforgery;
        _httpContextAccessor = httpContextAccessor;
        Options = options.Value;
    }

    public virtual void SetCookie()
    {
        HttpContext.Response.Cookies.Append(
            Options.TokenCookie.Name!,
            GenerateToken(),
            Options.TokenCookie.Build(HttpContext)
        );
    }

    public virtual string GenerateToken()
    {
        return _antiforgery.GetAndStoreTokens(_httpContextAccessor.HttpContext!).RequestToken!;
    }
}
