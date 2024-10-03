namespace Tiknas.AspNetCore.Mvc.UI.Bundling;

public interface IBundler
{
    string FileExtension { get; }

    BundleResult Bundle(IBundlerContext context);
}
