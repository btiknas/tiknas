using NUglify;
using Tiknas.Minify.Scripts;

namespace Tiknas.Minify.NUglify;

public class NUglifyJavascriptMinifier : NUglifyMinifierBase, IJavascriptMinifier
{
    protected override UglifyResult UglifySource(string source, string? fileName)
    {
        return Uglify.Js(source, fileName);
    }
}
