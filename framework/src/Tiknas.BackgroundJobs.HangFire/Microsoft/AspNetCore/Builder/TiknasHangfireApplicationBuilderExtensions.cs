using System;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.BackgroundJobs.Hangfire;

namespace Microsoft.AspNetCore.Builder;

public static class TiknasHangfireApplicationBuilderExtensions
{
    public static IApplicationBuilder UseTiknasHangfireDashboard(
        this IApplicationBuilder app,
        string pathMatch = "/hangfire",
        Action<DashboardOptions>? configure = null,
        JobStorage? storage = null)
    {
        var options = app.ApplicationServices.GetRequiredService<TiknasDashboardOptionsProvider>().Get();
        configure?.Invoke(options);
        return app.UseHangfireDashboard(pathMatch, options, storage);
    }
}