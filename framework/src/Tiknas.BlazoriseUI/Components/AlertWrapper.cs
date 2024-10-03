using Tiknas.AspNetCore.Components.Alerts;

namespace Tiknas.BlazoriseUI.Components;

internal class AlertWrapper
{
    public AlertMessage AlertMessage { get; set; } = default!;
    public bool IsVisible { get; set; }
}
