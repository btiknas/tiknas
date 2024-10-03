using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;

public class TiknasPageToolbarOptions
{
    public PageToolbarDictionary Toolbars { get; }

    public TiknasPageToolbarOptions()
    {
        Toolbars = new PageToolbarDictionary();
    }

    public void Configure<TPage>([NotNull] Action<PageToolbar> configureAction)
    {
        // ReSharper disable once AssignNullToNotNullAttribute
        Configure(typeof(TPage).FullName!, configureAction);
    }

    public void Configure([NotNull] string pageName, [NotNull] Action<PageToolbar> configureAction)
    {
        Check.NotNullOrWhiteSpace(pageName, nameof(pageName));
        Check.NotNull(configureAction, nameof(configureAction));

        var toolbar = Toolbars.GetOrAdd(pageName, () => new PageToolbar(pageName));
        configureAction(toolbar);
    }
}
