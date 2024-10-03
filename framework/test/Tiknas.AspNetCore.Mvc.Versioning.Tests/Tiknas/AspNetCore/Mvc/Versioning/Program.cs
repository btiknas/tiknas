using Microsoft.AspNetCore.Builder;
using Tiknas.AspNetCore;
using Tiknas.AspNetCore.Mvc.Versioning;
using Tiknas.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunTiknasModuleAsync<TiknasAspNetCoreMvcVersioningTestModule>();

public partial class Program
{
}
