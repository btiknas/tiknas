using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Tiknas.Autofac;
using Tiknas.Localization;
using Tiknas.Modularity;
using Tiknas.TextTemplating.Localization;
using Tiknas.TextTemplating.Razor;
using Tiknas.TextTemplating.Scriban;
using Tiknas.VirtualFileSystem;

namespace Tiknas.TextTemplating;

[DependsOn(
    typeof(TiknasTextTemplatingScribanModule),
    typeof(TiknasTextTemplatingRazorModule),
    typeof(TiknasTestBaseModule),
    typeof(TiknasAutofacModule),
    typeof(TiknasLocalizationModule)
)]
public class TiknasTextTemplatingTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TiknasTextTemplatingTestModule>("Tiknas.TextTemplating");
        });

        Configure<TiknasLocalizationOptions>(options =>
        {
            options.Resources
                .Add<TestLocalizationSource>("en")
                .AddVirtualJson("/Localization");
        });

        Configure<TiknasCompiledViewProviderOptions>(options =>
        {
            options.TemplateReferences.Add(TestTemplates.HybridTemplateRazor,
                new List<PortableExecutableReference>()
                {
                        MetadataReference.CreateFromFile(typeof(TiknasTextTemplatingTestModule).Assembly.Location)
                });
        });
    }
}
