using Microsoft.AspNetCore.Builder;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared;

public static class TiknasApplicationBuilderErrorPageExtensions
{
    public static IApplicationBuilder UseErrorPage(this IApplicationBuilder app)
    {
        return app
            .UseStatusCodePagesWithRedirects("~/Error?httpStatusCode={0}")
            .UseExceptionHandler("/Error");
    }
}
