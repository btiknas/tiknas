using System.Threading.Tasks;

namespace Tiknas.Cli.LIbs;

public interface IInstallLibsService
{
    Task InstallLibsAsync(string directory);
}
