using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tiknas.Threading;

namespace Tiknas.AspNetCore.TestBase;

internal class TestStartup<TStartupModule>
{
    public void ConfigureServices(IServiceCollection services)
    {
        AsyncHelper.RunSync(() => services.AddApplicationAsync(typeof(TStartupModule)));
    }

    public void Configure(IApplicationBuilder app)
    {
        AsyncHelper.RunSync(app.InitializeApplicationAsync);
    }
}