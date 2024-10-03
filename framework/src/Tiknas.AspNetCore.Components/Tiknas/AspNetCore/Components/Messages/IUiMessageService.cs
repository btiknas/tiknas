using System;
using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Components.Messages;

public interface IUiMessageService
{
    Task Info(string message, string? title = null, Action<UiMessageOptions>? options = null);

    Task Success(string message, string? title = null, Action<UiMessageOptions>? options = null);

    Task Warn(string message, string? title = null, Action<UiMessageOptions>? options = null);

    Task Error(string message, string? title = null, Action<UiMessageOptions>? options = null);

    Task<bool> Confirm(string message, string? title = null, Action<UiMessageOptions>? options = null);
}
