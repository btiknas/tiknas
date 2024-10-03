using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Components.Web.Theming.Toolbars;

public interface IToolbarManager
{
    Task<Toolbar> GetAsync(string name);
}
