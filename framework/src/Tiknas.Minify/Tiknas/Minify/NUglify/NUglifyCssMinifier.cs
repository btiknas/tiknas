using NUglify;
using Tiknas.Minify.Styles;

namespace Tiknas.Minify.NUglify;

public class NUglifyCssMinifier : NUglifyMinifierBase, ICssMinifier
{
    protected override UglifyResult UglifySource(string source, string? fileName)
    {
        return Uglify.Css(source, fileName);
    }
}
