using Microsoft.Extensions.DependencyInjection;
using Tiknas.Autofac;
using Tiknas.AutoMapper;
using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.ObjectMapping;
using Tiknas.Settings;

namespace Tiknas.MultiLingualObjects;

[DependsOn(
    typeof(TiknasAutofacModule),
    typeof(TiknasLocalizationModule),
    typeof(TiknasSettingsModule),
    typeof(TiknasObjectMappingModule),
    typeof(TiknasMultiLingualObjectsModule),
    typeof(TiknasTestBaseModule),
    typeof(TiknasAutoMapperModule)
)]
public class TiknasMultiLingualObjectsTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasSettingOptions>(options =>
        {   
            options.DefinitionProviders.Add<LocalizationSettingProvider>();
        });
        context.Services.AddAutoMapperObjectMapper<TiknasMultiLingualObjectsTestModule>();
        Configure<TiknasAutoMapperOptions>(options =>
        {
            options.AddMaps<TiknasMultiLingualObjectsTestModule>(validate: true);
        });
    }
}
