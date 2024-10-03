using System.Threading.Tasks;

namespace Tiknas.UI.Navigation;

public interface IMenuManager
{
    Task<ApplicationMenu> GetAsync(string name);

    Task<ApplicationMenu> GetMainMenuAsync();
}
