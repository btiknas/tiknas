using System.Threading.Tasks;

namespace Tiknas.Cli.Configuration;

public interface IConfigReader
{
    TiknasCliConfig Read(string directory);
}
