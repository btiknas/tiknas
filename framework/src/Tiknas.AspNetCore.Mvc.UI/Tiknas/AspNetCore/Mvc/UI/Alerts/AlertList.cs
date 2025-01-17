﻿using System.Collections.Generic;

namespace Tiknas.AspNetCore.Mvc.UI.Alerts;

public class AlertList : List<AlertMessage>
{
    public void Add(AlertType type, string text, string? title = null, bool dismissible = true)
    {
        Add(new AlertMessage(type, text, title, dismissible));
    }

    public void Info(string text, string? title = null, bool dismissible = true)
    {
        Add(new AlertMessage(AlertType.Info, text, title, dismissible));
    }

    public void Warning(string text, string? title = null, bool dismissible = true)
    {
        Add(new AlertMessage(AlertType.Warning, text, title, dismissible));
    }

    public void Danger(string text, string? title = null, bool dismissible = true)
    {
        Add(new AlertMessage(AlertType.Danger, text, title, dismissible));
    }

    public void Success(string text, string? title = null, bool dismissible = true)
    {
        Add(new AlertMessage(AlertType.Success, text, title, dismissible));
    }
}
