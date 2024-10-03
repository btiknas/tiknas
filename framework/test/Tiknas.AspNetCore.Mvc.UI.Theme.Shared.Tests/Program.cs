using Microsoft.AspNetCore.Builder;
using Tiknas.AspNetCore;
using Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Tests.Tiknas.AspNetCore.Mvc.UI.Theme.Shared;
using Tiknas.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunTiknasModuleAsync<TiknasAspNetCoreMvcUiThemeSharedTestModule>();

public partial class Program
{
}
