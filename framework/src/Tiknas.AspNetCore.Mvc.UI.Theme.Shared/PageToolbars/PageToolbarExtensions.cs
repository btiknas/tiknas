using System;
using Localization.Resources.TiknasUi;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;
using Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Pages.Shared.Components.TiknasPageToolbar.Button;
using Tiknas.Localization;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;

public static class PageToolbarExtensions
{
    public static PageToolbar AddComponent<TComponent>(
        this PageToolbar toolbar,
        object? argument = null,
        int order = 0,
        string? requiredPolicyName = null)
    {
        return toolbar.AddComponent(
            typeof(TComponent),
            argument,
            order,
            requiredPolicyName
        );
    }

    public static PageToolbar AddComponent(
        this PageToolbar toolbar,
        Type componentType,
        object? argument = null,
        int order = 0,
        string? requiredPolicyName = null)
    {
        toolbar.Contributors.Add(
            new SimplePageToolbarContributor(
                componentType,
                argument,
                order,
                requiredPolicyName
            )
        );

        return toolbar;
    }

    public static PageToolbar AddButton(
        this PageToolbar toolbar,
        ILocalizableString text,
        string? icon = null,
        string? name = null,
        string? id = null,
        ILocalizableString? busyText = null,
        FontIconType iconType = FontIconType.FontAwesome,
        TiknasButtonType type = TiknasButtonType.Primary,
        TiknasButtonSize size = TiknasButtonSize.Small,
        bool disabled = false,
        int order = 0,
        string? requiredPolicyName = null)
    {
        if (busyText == null)
        {
            busyText = new LocalizableString(typeof(TiknasUiResource), "ProcessingWithThreeDot");
        }

        toolbar.AddComponent<TiknasPageToolbarButtonViewComponent>(
            new {
                text,
                icon,
                name,
                id,
                busyText,
                iconType,
                type,
                size,
                disabled
            },
            order,
            requiredPolicyName
        );

        return toolbar;
    }
}
