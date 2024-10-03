using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.VirtualFileSystem;
using Tiknas.Minify.Styles;

namespace Tiknas.AspNetCore.Mvc.UI.Bundling.Styles;

public class StyleBundler : BundlerBase, IStyleBundler
{
    private readonly IWebHostEnvironment _hostingEnvironment;
    public override string FileExtension => "css";

    public StyleBundler(
        IWebHostEnvironment hostEnvironment,
        ICssMinifier minifier,
        IOptions<TiknasBundlingOptions> bundlingOptions)
        : base(
            hostEnvironment,
            minifier,
            bundlingOptions)
    {
        _hostingEnvironment = hostEnvironment;
    }

    public string GetAbsolutePath(string relativePath)
    {
        return Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", relativePath.RemovePreFix("/"));
    }

    protected override string ProcessBeforeAddingToTheBundle(IBundlerContext context, string filePath, string fileContent)
    {
        return CssRelativePath.Adjust(
            fileContent,
            GetAbsolutePath(filePath),
            GetAbsolutePath(context.BundleRelativePath)
        );
    }
}
