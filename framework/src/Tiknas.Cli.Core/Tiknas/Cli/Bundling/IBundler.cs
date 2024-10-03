using Tiknas.Bundling;

namespace Tiknas.Cli.Bundling;

public interface IBundler
{
    string Bundle(BundleOptions options, BundleContext context);
}
