using System.Threading.Tasks;
using Blazorise;

namespace Tiknas.BlazoriseUI;

public static class TiknasBlazoriseUiModalExtensions
{
    public static Task CancelClosingModalWhenFocusLost(this Modal modal, ModalClosingEventArgs eventArgs)
    {
        // cancel close if clicked outside of modal area
        eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;

        return Task.CompletedTask;
    }
}
