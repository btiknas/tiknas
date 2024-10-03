using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas.Application;
using Tiknas.Autofac;
using Tiknas.Modularity;
using Tiknas.TestApp.Domain;
using Tiknas.AutoMapper;
using Tiknas.Domain.Entities.Caching;
using Tiknas.Domain.Entities.Events.Distributed;
using Tiknas.TestApp.Application.Dto;
using Tiknas.TestApp.Testing;
using Tiknas.Threading;

namespace Tiknas.TestApp;

[DependsOn(
    typeof(TiknasDddApplicationModule),
    typeof(TiknasAutofacModule),
    typeof(TiknasTestBaseModule),
    typeof(TiknasAutoMapperModule)
    )]
public class TestAppModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureAutoMapper();
        ConfigureDistributedEventBus();
        
        context.Services.Replace(ServiceDescriptor.Singleton<IDistributedCache, TestMemoryDistributedCache>());
        context.Services.AddEntityCache<Product, Guid>();
        context.Services.AddEntityCache<Product, ProductCacheItem, Guid>();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        SeedTestData(context);
    }

    private void ConfigureAutoMapper()
    {
        Configure<TiknasAutoMapperOptions>(options =>
        {
            options.Configurators.Add(ctx =>
            {
                ctx.MapperConfiguration.CreateMap<Person, PersonDto>().ReverseMap();
                ctx.MapperConfiguration.CreateMap<Phone, PhoneDto>().ReverseMap();
            });

            options.AddMaps<TestAppModule>();
        });
    }

    private void ConfigureDistributedEventBus()
    {
        Configure<TiknasDistributedEntityEventOptions>(options =>
        {
            options.AutoEventSelectors.Add<Person>();
            options.EtoMappings.Add<Person, PersonEto>();
        });
    }

    private static void SeedTestData(ApplicationInitializationContext context)
    {
        using (var scope = context.ServiceProvider.CreateScope())
        {
            AsyncHelper.RunSync(() => scope.ServiceProvider
                .GetRequiredService<TestDataBuilder>()
                .BuildAsync());
        }
    }
}
