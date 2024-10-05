using System.Threading.Tasks;
using Shouldly;
using Tiknas.TextTemplating.VirtualFiles;
using Xunit;

namespace Tiknas.TextTemplating.Scriban.SampleTemplates;

public class ScribanTemplateContentFileProvider_Tests : TemplateContentFileProvider_Tests<ScribanTextTemplatingTestModule>
{
    [Fact]
    public async Task GetScribanFilesAsync()
    {
        var definition = await TemplateDefinitionManager.GetAsync(TestTemplates.WelcomeEmail);
        var files = await TemplateContentFileProvider.GetFilesAsync(definition);
        files.Count.ShouldBe(2);
        files.ShouldContain(x => x.FileName == "en.tpl"  && x.FileContent.Contains("Welcome {{model.name}} to the tiknas.de!"));
        files.ShouldContain(x => x.FileName == "tr.tpl"  && x.FileContent.Contains("Merhaba {{model.name}}, tiknas.de'ya hoşgeldiniz!"));
    }
}
