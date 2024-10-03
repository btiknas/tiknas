using Microsoft.AspNetCore.Builder;
using Tiknas.AspNetCore;
using Tiknas.AspNetCore.TestBase;
using Tiknas.Http;

var builder = WebApplication.CreateBuilder();
await builder.RunTiknasModuleAsync<TiknasHttpClientTestModule>();

public partial class Program
{
}
