using System.Threading.Tasks;

namespace Tiknas.UI.Navigation;

public interface IMenuContributor
{
    Task ConfigureMenuAsync(MenuConfigurationContext context);
}
