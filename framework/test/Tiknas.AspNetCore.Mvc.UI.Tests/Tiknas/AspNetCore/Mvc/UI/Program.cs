using Microsoft.AspNetCore.Builder;
using Tiknas.AspNetCore;
using Tiknas.AspNetCore.Mvc.UI;
using Tiknas.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunTiknasModuleAsync<TiknasAspNetCoreMvcUiTestModule>();

public partial class Program
{
}
