using System.Threading.Tasks;
using Shouldly;
using Tiknas.Modularity;
using Xunit;

namespace Tiknas.TextTemplating.VirtualFiles;

public abstract class VirtualFileTemplateContributor_Tests<TStartupModule> : TiknasTextTemplatingTestBase<TStartupModule>
    where TStartupModule : ITiknasModule
{
    protected readonly ITemplateDefinitionManager TemplateDefinitionManager;
    protected readonly VirtualFileTemplateContentContributor VirtualFileTemplateContentContributor;
    protected string WelcomeEmailEnglishContent;
    protected string WelcomeEmailTurkishContent;
    protected string ForgotPasswordEmailEnglishContent;

    protected VirtualFileTemplateContributor_Tests()
    {
        TemplateDefinitionManager = GetRequiredService<ITemplateDefinitionManager>();
        VirtualFileTemplateContentContributor = GetRequiredService<VirtualFileTemplateContentContributor>();
    }

    [Fact]
    public async Task Should_Get_Localized_Content_By_Culture()
    {
        (await VirtualFileTemplateContentContributor.GetOrNullAsync(
                new TemplateContentContributorContext(await TemplateDefinitionManager.GetAsync(TestTemplates.WelcomeEmail),
                    ServiceProvider,
                    "en")))
            .ShouldBe(WelcomeEmailEnglishContent);

        (await VirtualFileTemplateContentContributor.GetOrNullAsync(
                new TemplateContentContributorContext(await TemplateDefinitionManager.GetAsync(TestTemplates.WelcomeEmail),
                    ServiceProvider,
                    "tr")))
            .ShouldBe(WelcomeEmailTurkishContent);
    }

    [Fact]
    public async Task Should_Get_Non_Localized_Template_Content()
    {
        var actualContent = await VirtualFileTemplateContentContributor.GetOrNullAsync(
            new TemplateContentContributorContext(
                await TemplateDefinitionManager.GetAsync(TestTemplates.ForgotPasswordEmail),
                ServiceProvider,
                null));

        // Normalisiere beide Inhalte, um alle Zeilenumbrüche zu entfernen
        var normalizedActualContent = NormalizeNewLines(actualContent);
        var normalizedExpectedContent = NormalizeNewLines(ForgotPasswordEmailEnglishContent);

        normalizedActualContent.ShouldBe(normalizedExpectedContent);
    }

    private string NormalizeNewLines(string input)
    {
        // Entferne alle Zeilenumbrüche, sodass der Vergleich konsistent ist
        return input.Replace("\r\n", "\n").Replace("\r", "\n");
    }
}