using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;

public interface IToolbarManager
{
    Task<Toolbar> GetAsync(string name);
}
