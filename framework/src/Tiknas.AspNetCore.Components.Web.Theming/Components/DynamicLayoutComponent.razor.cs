using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace Tiknas.AspNetCore.Components.Web.Theming.Components;

public partial class DynamicLayoutComponent : ComponentBase
{
    [Inject]
    protected IOptions<TiknasDynamicLayoutComponentOptions> TiknasDynamicLayoutComponentOptions { get; set; } = default!;
}