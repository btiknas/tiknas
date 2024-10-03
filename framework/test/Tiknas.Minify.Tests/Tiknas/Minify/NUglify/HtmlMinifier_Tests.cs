using Shouldly;
using Tiknas.Minify.Html;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.Minify.NUglify;

public class HtmlMinifier_Tests : TiknasIntegratedTest<TiknasMinifyModule>
{
    private readonly IHtmlMinifier _htmlMinifier;

    public HtmlMinifier_Tests()
    {
        _htmlMinifier = GetRequiredService<IHtmlMinifier>();
    }

    [Fact]
    public void Should_Minify_Simple_Code()
    {
        const string source = "<div>  <p>This is <em>   a text    </em></p>   </div>";

        var minified = _htmlMinifier.Minify(source);

        minified.Length.ShouldBeGreaterThan(0);
        minified.Length.ShouldBeLessThan(source.Length);
    }
}
