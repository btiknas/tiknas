using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Tiknas.AspNetCore;
using Tiknas.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    EnvironmentName = Environments.Staging
});
await builder.RunTiknasModuleAsync<TiknasAspNetCoreTestModule>();

public partial class Program
{
}
