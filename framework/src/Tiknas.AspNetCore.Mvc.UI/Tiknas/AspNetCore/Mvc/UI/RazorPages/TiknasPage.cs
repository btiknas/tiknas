using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tiknas.Users;

namespace Tiknas.AspNetCore.Mvc.UI.RazorPages;

public abstract class TiknasPage : Page
{
    [RazorInject]
    public ICurrentUser CurrentUser { get; set; } = default!;
}
