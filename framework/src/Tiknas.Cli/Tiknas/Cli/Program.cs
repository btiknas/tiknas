using System;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System.IO;
using System.Threading.Tasks;

namespace Tiknas.Cli;

public class Program
{
    private static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var loggerOutputTemplate = "{Message:lj}{NewLine}{Exception}";
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Tiknas", LogEventLevel.Warning)
            .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
            .MinimumLevel.Override("Tiknas.IdentityModel", LogEventLevel.Information)
#if DEBUG
            .MinimumLevel.Override("Tiknas.Cli", LogEventLevel.Debug)
#else
            .MinimumLevel.Override("Tiknas.Cli", LogEventLevel.Information)
#endif
            .Enrich.FromLogContext()
            .WriteTo.File(Path.Combine(CliPaths.Log, "tiknas-cli-logs.txt"), outputTemplate: loggerOutputTemplate)
            .WriteTo.Console(theme: AnsiConsoleTheme.Sixteen, outputTemplate: loggerOutputTemplate)
            .CreateLogger();

        using (var application = TiknasApplicationFactory.Create<TiknasCliModule>(
            options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
            }))
        {
            application.Initialize();

            await application.ServiceProvider
                .GetRequiredService<CliService>()
                .RunAsync(args);

            application.Shutdown();

            Log.CloseAndFlush();
        }
    }
}
