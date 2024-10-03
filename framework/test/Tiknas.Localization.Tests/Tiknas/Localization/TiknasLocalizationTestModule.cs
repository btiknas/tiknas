using Tiknas.Localization.TestResources.Base.Validation;
using Tiknas.Localization.TestResources.External;
using Tiknas.Localization.TestResources.Source;
using Tiknas.Modularity;
using Tiknas.VirtualFileSystem;

namespace Tiknas.Localization;

[DependsOn(typeof(TiknasTestBaseModule))]
[DependsOn(typeof(TiknasLocalizationModule))]
public class TiknasLocalizationTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasLocalizationTestModule>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.DefaultResourceType = typeof(LocalizationTestResource);

            options.Resources
                .Add<LocalizationTestValidationResource>("en")
                .AddVirtualJson("/Tiknas/Localization/TestResources/Base/Validation");

            options.Resources
                .Add("LocalizationTestCountryNames")
                .AddVirtualJson("/Tiknas/Localization/TestResources/Base/CountryNames");

            options.Resources
                .Add<LocalizationTestResource>("en")
                .AddVirtualJson("/Tiknas/Localization/TestResources/Source")
                .AddBaseResources("LocalizationTestCountryNames");

            options.Resources
                .Get<LocalizationTestResource>()
                .AddVirtualJson("/Tiknas/Localization/TestResources/SourceExt");

            options.GlobalContributors.Add<TestExternalLocalizationContributor>();
        });
    }
}
