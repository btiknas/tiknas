using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Components.Web.Theming.PageToolbars;

public interface IPageToolbarManager
{
    Task<PageToolbarItem[]> GetItemsAsync(PageToolbar toolbar);
}
