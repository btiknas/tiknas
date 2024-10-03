using System;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;
using Tiknas.Swashbuckle;

namespace Microsoft.AspNetCore.Builder;

public static class TiknasSwaggerUIBuilderExtensions
{
    public static IApplicationBuilder UseTiknasSwaggerUI(
        this IApplicationBuilder app,
        Action<SwaggerUIOptions>? setupAction = null)
    {
        var resolver = app.ApplicationServices.GetService<ISwaggerHtmlResolver>();

        return app.UseSwaggerUI(options =>
        {
            options.InjectJavascript("ui/tiknas.js");
            options.InjectJavascript("ui/tiknas.swagger.js");
            options.IndexStream = () => resolver?.Resolver();

            setupAction?.Invoke(options);
        });
    }
}
