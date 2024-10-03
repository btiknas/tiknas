using Tiknas.UI.Navigation;

namespace Tiknas.UI.Navigation;

public interface IHasMenuItems
{
    /// <summary>
    /// Menu items.
    /// </summary>
    ApplicationMenuItemList Items { get; }
}
