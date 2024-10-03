using Shouldly;
using Tiknas.Minify.Scripts;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.Minify.NUglify;

public class JavascriptMinifier_Tests : TiknasIntegratedTest<TiknasMinifyModule>
{
    private readonly IJavascriptMinifier _javascriptMinifier;

    public JavascriptMinifier_Tests()
    {
        _javascriptMinifier = GetRequiredService<IJavascriptMinifier>();
    }

    [Fact]
    public void Should_Minify_Simple_Code()
    {
        const string source = "var x = 5; var y = 6;";

        var minified = _javascriptMinifier.Minify(source);

        minified.Length.ShouldBeGreaterThan(0);
        minified.Length.ShouldBeLessThan(source.Length);
    }
}
