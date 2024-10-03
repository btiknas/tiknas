using Tiknas.Data;
using Tiknas.ExceptionHandling.Localization;
using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.VirtualFileSystem;

namespace Tiknas.ExceptionHandling;

/* TODO: This package is introduced in a later time by gathering
 * classes from multiple packages.
 * So, didn't change the original namespaces of the types to not introduce breaking changes.
 * We will re-design this package in a later time, probably with v5.0.
 */
[DependsOn(
    typeof(TiknasLocalizationModule),
    typeof(TiknasDataModule)
    )]
public class TiknasExceptionHandlingModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasExceptionHandlingModule>();
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.Resources
                .Add<TiknasExceptionHandlingResource>("en")
                .AddVirtualJson("/Tiknas/ExceptionHandling/Localization");
        });
    }
}
