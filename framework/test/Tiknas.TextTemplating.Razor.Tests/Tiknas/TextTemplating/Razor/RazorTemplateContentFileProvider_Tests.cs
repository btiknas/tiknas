using System.Threading.Tasks;
using Shouldly;
using Tiknas.TextTemplating.VirtualFiles;
using Xunit;

namespace Tiknas.TextTemplating.Razor;

public class RazorTemplateContentFileProvider_Tests : TemplateContentFileProvider_Tests<RazorTextTemplatingTestModule>
{
    [Fact]
    public async Task GetRazorFilesAsync()
    {
        var definition = await TemplateDefinitionManager.GetAsync(TestTemplates.WelcomeEmail);
        var files = await TemplateContentFileProvider.GetFilesAsync(definition);
        files.Count.ShouldBe(2);
        files.ShouldContain(x => x.FileName == "en.cshtml"  && x.FileContent.Contains("Welcome @Model.Name to the tiknas.io!"));
        files.ShouldContain(x => x.FileName == "tr.cshtml"  && x.FileContent.Contains("Merhaba @Model.Name, tiknas.io'ya hoşgeldiniz!"));
    }
}
