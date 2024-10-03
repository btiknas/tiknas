using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.Modularity;
using Tiknas.TestApp.MemoryDb;
using Tiknas.Data;
using Tiknas.Autofac;
using Tiknas.Domain.Repositories.MemoryDb;
using Tiknas.Json.SystemTextJson;
using Tiknas.MemoryDb.JsonConverters;
using Tiknas.TestApp;
using Tiknas.TestApp.Domain;

namespace Tiknas.MemoryDb;

[DependsOn(
    typeof(TestAppModule),
    typeof(TiknasMemoryDbModule),
    typeof(TiknasAutofacModule))]
public class TiknasMemoryDbTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var connStr = Guid.NewGuid().ToString();

        Configure<TiknasDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = connStr;
        });

        context.Services.AddMemoryDbContext<TestAppMemoryDbContext>(options =>
        {
            options.AddDefaultRepositories();
            options.AddRepository<City, CityRepository>();
        });

        context.Services.AddOptions<Utf8JsonMemoryDbSerializerOptions>()
            .Configure<IServiceProvider>((options, rootServiceProvider) =>
            {
                options.JsonSerializerOptions.Converters.Add(new EntityJsonConverter<EntityWithIntPk, int>());
                options.JsonSerializerOptions.TypeInfoResolver = new TiknasDefaultJsonTypeInfoResolver(rootServiceProvider
                    .GetRequiredService<IOptions<TiknasSystemTextJsonSerializerModifiersOptions>>());
            });
    }
}
