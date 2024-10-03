using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shouldly;
using Xunit;

namespace Tiknas.AspNetCore;

public class TiknasHostEnvironment_Tests : TiknasAspNetCoreTestBase<Program>
{
    [Fact]
    public void Should_Set_Environment_From_IWebHostEnvironment()
    {
        var tiknasHostEnvironment = GetRequiredService<ITiknasHostEnvironment>();
        tiknasHostEnvironment.EnvironmentName.ShouldBe(Environments.Staging);
    }
}

public class TiknasHostEnvironment_Async_Initialize_Tests
{
    [Fact]
    public async Task Should_Set_Environment_From_AspNetCore()
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            EnvironmentName = Environments.Staging
        });
        builder.Host.UseAutofac();
        await builder.AddApplicationAsync<TiknasAspNetCoreTestModule>();
        var app = builder.Build();
        await app.InitializeApplicationAsync();

        var tiknasHostEnvironment = app.Services.GetRequiredService<ITiknasHostEnvironment>();
        tiknasHostEnvironment.EnvironmentName.ShouldBe(Environments.Staging);

        builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            EnvironmentName = Environments.Staging
        });
        builder.Host.UseAutofac();
        var tiknasApp = await TiknasApplicationFactory.CreateAsync<TiknasAspNetCoreTestModule>(builder.Services);
        app = builder.Build();
        await app.InitializeApplicationAsync();

        tiknasHostEnvironment = tiknasApp.Services.GetRequiredService<ITiknasHostEnvironment>();
        tiknasHostEnvironment.EnvironmentName.ShouldBe(Environments.Staging);
    }
}
