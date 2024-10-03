using Tiknas.Cli.Bundling;

namespace Tiknas.Cli.Configuration;

public class TiknasCliConfig
{
    public BundleConfig Bundle { get; set; } = new();
}
