using Microsoft.Extensions.Options;
using Shouldly;
using Xunit;

namespace Tiknas.TextTemplating;

public class TiknasTextTemplatingOptions_Tests : TiknasTextTemplatingTestBase<TiknasTextTemplatingTestModule>
{
    private readonly TiknasTextTemplatingOptions _options;

    public TiknasTextTemplatingOptions_Tests()
    {
        _options = GetRequiredService<IOptions<TiknasTextTemplatingOptions>>().Value;
    }

    [Fact]
    public void Should_Auto_Add_TemplateDefinitionProviders_To_Options()
    {
        _options
            .DefinitionProviders
            .ShouldContain(typeof(TestTemplateDefinitionProvider));
    }
}
