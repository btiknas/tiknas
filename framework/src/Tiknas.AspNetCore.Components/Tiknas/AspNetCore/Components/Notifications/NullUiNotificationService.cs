﻿using System;
using System.Threading.Tasks;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.Notifications;

public class NullUiNotificationService : IUiNotificationService, ITransientDependency
{
    public Task Info(string message, string? title = null, Action<UiNotificationOptions>? options = null)
    {
        return Task.CompletedTask;
    }

    public Task Success(string message, string? title = null, Action<UiNotificationOptions>? options = null)
    {
        return Task.CompletedTask;
    }

    public Task Warn(string message, string? title = null, Action<UiNotificationOptions>? options = null)
    {
        return Task.CompletedTask;
    }
    public Task Error(string message, string? title = null, Action<UiNotificationOptions>? options = null)
    {
        return Task.CompletedTask;
    }
}
