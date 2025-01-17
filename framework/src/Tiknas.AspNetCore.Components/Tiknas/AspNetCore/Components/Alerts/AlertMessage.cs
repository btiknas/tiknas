﻿using JetBrains.Annotations;

namespace Tiknas.AspNetCore.Components.Alerts;

public class AlertMessage
{
    [NotNull]
    public string Text {
        get => _text;
        set => _text = Check.NotNullOrWhiteSpace(value, nameof(value));
    }
    private string _text = default!;

    public AlertType Type { get; set; }

    public string? Title { get; set; }

    public bool Dismissible { get; set; }

    public AlertMessage(AlertType type, [NotNull] string text, string? title = null, bool dismissible = true)
    {
        Type = type;
        Text = Check.NotNullOrWhiteSpace(text, nameof(text));
        Title = title;
        Dismissible = dismissible;
    }
}
