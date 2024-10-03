using NUglify;
using Tiknas.Minify.Html;

namespace Tiknas.Minify.NUglify;

public class NUglifyHtmlMinifier : NUglifyMinifierBase, IHtmlMinifier
{
    protected override UglifyResult UglifySource(string source, string? fileName)
    {
        return Uglify.Html(source, sourceFileName: fileName);
    }
}
