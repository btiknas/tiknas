using Shouldly;
using Tiknas.Minify.Styles;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.Minify.NUglify;

public class CssMinifier_Tests : TiknasIntegratedTest<TiknasMinifyModule>
{
    private readonly ICssMinifier _cssMinifier;

    public CssMinifier_Tests()
    {
        _cssMinifier = GetRequiredService<ICssMinifier>();
    }

    [Fact]
    public void Should_Minify_Simple_Code()
    {
        const string source = "div { color: #FFF; }";

        var minified = _cssMinifier.Minify(source);

        minified.Length.ShouldBeGreaterThan(0);
        minified.Length.ShouldBeLessThan(source.Length);
    }
}
