using Microsoft.AspNetCore.Builder;
using Tiknas.AspNetCore.SignalR;
using Tiknas.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunTiknasModuleAsync<TiknasAspNetCoreSignalRTestModule>();

public partial class Program
{
}
