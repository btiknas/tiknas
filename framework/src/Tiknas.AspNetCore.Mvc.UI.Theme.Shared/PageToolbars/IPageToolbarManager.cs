using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;

public interface IPageToolbarManager
{
    Task<PageToolbarItem[]> GetItemsAsync(string pageName);
}
