using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.AspNetCore.TestBase;
using Tiknas.Autofac;
using Tiknas.Modularity;
using Xunit;

namespace Tiknas.AspNetCore.Mvc;

public class TiknasAspNetCoreAsyncTestBase_Tests : TiknasAspNetCoreAsyncTestBase<TiknasAspNetCoreAsyncTestModule>
{
    [Fact]
    public async Task Get_API_Response_Test()
    {
        var result = await GetResponseAsStringAsync("/api");
        result.ShouldBe(await GetRequiredService<DataBaseService>().GetResponseAsync());
    }
}

[DependsOn(
    typeof(TiknasAspNetCoreMvcModule),
    typeof(TiknasAspNetCoreTestBaseModule),
    typeof(TiknasAutofacModule)
)]
public class TiknasAspNetCoreAsyncTestModule : TiknasModule
{
    public override Task ConfigureServicesAsync(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<DataBaseService>();
        return Task.CompletedTask;
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        var dataBaseService = app.ApplicationServices.GetRequiredService<DataBaseService>();

        var apiResponse = await dataBaseService.GetResponseAsync();
        app.Map("/api", _ =>
        {
            app.Run(async httpContext =>
            {
                await httpContext.Response.WriteAsync(apiResponse);
            });
        });

        app.UseRouting();
        app.UseConfiguredEndpoints();
    }
}

public class DataBaseService
{
    public Task<string> GetResponseAsync()
    {
        return Task.FromResult("hello api!");
    }
}
