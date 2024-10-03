using System.Threading.Tasks;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.BlockUi;

public class NullBlockUiService : IBlockUiService, ISingletonDependency
{
    public Task Block(string? selectors, bool busy = false)
    {
        return Task.CompletedTask;
    }

    public Task UnBlock()
    {
        return Task.CompletedTask;
    }
}
