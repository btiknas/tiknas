using Localization.Resources.TiknasUi;
using Tiknas.ExceptionHandling;
using Tiknas.ExceptionHandling.Localization;
using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.VirtualFileSystem;

namespace Tiknas.UI;

[DependsOn(
    typeof(TiknasExceptionHandlingModule)
)]
public class TiknasUiModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasUiModule>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.Resources
                .Add<TiknasUiResource>("en")
                .AddBaseTypes(typeof(TiknasExceptionHandlingResource))
                .AddVirtualJson("/Localization/Resources/TiknasUi");
        });
    }
}
