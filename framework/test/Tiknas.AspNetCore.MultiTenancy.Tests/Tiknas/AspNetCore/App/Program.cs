using Microsoft.AspNetCore.Builder;
using Tiknas.AspNetCore;
using Tiknas.AspNetCore.App;
using Tiknas.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunTiknasModuleAsync<AppModule>();

public partial class Program
{
}
