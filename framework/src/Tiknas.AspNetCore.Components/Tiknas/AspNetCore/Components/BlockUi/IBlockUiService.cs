using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Components.BlockUi;

public interface IBlockUiService
{
    Task Block(string? selectors, bool busy = false);

    Task UnBlock();
}
