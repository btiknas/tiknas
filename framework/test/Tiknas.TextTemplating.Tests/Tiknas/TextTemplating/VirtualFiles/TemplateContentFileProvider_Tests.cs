using System.Threading.Tasks;
using Shouldly;
using Tiknas.Modularity;
using Xunit;

namespace Tiknas.TextTemplating.VirtualFiles;

public abstract class TemplateContentFileProvider_Tests<TStartupModule> : TiknasTextTemplatingTestBase<TStartupModule>
    where TStartupModule : ITiknasModule
{
    protected readonly TemplateContentFileProvider TemplateContentFileProvider;
    protected readonly ITemplateDefinitionManager TemplateDefinitionManager;

    protected TemplateContentFileProvider_Tests()
    {
        TemplateContentFileProvider = GetRequiredService<TemplateContentFileProvider>();
        TemplateDefinitionManager = GetRequiredService<ITemplateDefinitionManager>();
    }

    [Fact]
    public async Task GetFilesAsync()
    {
        var definition = await TemplateDefinitionManager.GetAsync(TestTemplates.HybridTemplateScriban);

        var files = await TemplateContentFileProvider.GetFilesAsync(definition);
        files.Count.ShouldBe(1);
        files.ShouldContain(x => x.FileName == "TestScribanTemplate.tpl" && x.FileContent == "Hello {{model.name}}, {{L \"HowAreYou\" }}");
    }
}
