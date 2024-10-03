using System;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tiknas.Auditing;
using Tiknas.Modularity;
using Tiknas.ObjectExtending;
using Tiknas.ObjectMapping;

namespace Tiknas.AutoMapper;

[DependsOn(
    typeof(TiknasObjectMappingModule),
    typeof(TiknasObjectExtendingModule),
    typeof(TiknasAuditingModule)
)]
public class TiknasAutoMapperModule : TiknasModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddConventionalRegistrar(new TiknasAutoMapperConventionalRegistrar());
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper();

        context.Services.AddSingleton<IConfigurationProvider>(sp =>
        {
            using (var scope = sp.CreateScope())
            {
                var options = scope.ServiceProvider.GetRequiredService<IOptions<TiknasAutoMapperOptions>>().Value;

                var mapperConfigurationExpression = sp.GetRequiredService<IOptions<MapperConfigurationExpression>>().Value;
                var autoMapperConfigurationContext = new TiknasAutoMapperConfigurationContext(mapperConfigurationExpression, scope.ServiceProvider);

                foreach (var configurator in options.Configurators)
                {
                    configurator(autoMapperConfigurationContext);
                }
                var mapperConfiguration = new MapperConfiguration(mapperConfigurationExpression);

                foreach (var profileType in options.ValidatingProfiles)
                {
                    mapperConfiguration.Internal().AssertConfigurationIsValid(((Profile)Activator.CreateInstance(profileType)!).ProfileName);
                }

                return mapperConfiguration;
            }
        });

        context.Services.AddTransient<IMapper>(sp => sp.GetRequiredService<IConfigurationProvider>().CreateMapper(sp.GetService));

        context.Services.AddTransient<MapperAccessor>(sp => new MapperAccessor()
        {
            Mapper = sp.GetRequiredService<IMapper>()
        });
        context.Services.AddTransient<IMapperAccessor>(provider => provider.GetRequiredService<MapperAccessor>());
    }
}
