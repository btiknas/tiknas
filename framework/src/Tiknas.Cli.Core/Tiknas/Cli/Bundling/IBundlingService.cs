using System.Threading.Tasks;

namespace Tiknas.Cli.Bundling;

public interface IBundlingService
{
    Task BundleAsync(string directory, bool forceBuild, string projectType = BundlingConsts.WebAssembly);
}
