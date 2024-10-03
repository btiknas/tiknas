using Microsoft.CodeAnalysis;
using Tiknas.Modularity;
using Tiknas.VirtualFileSystem;

namespace Tiknas.TextTemplating.Razor;

[DependsOn(
    typeof(TiknasTextTemplatingTestModule)
)]
public class RazorTextTemplatingTestModule : TiknasModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TiknasVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<RazorTextTemplatingTestModule>("Tiknas.TextTemplating.Razor");
        });

        Configure<TiknasRazorTemplateCSharpCompilerOptions>(options =>
        {
            options.References.Add(MetadataReference.CreateFromFile(typeof(RazorTextTemplatingTestModule).Assembly.Location));
        });
    }
}
