using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;
using Tiknas.Localization;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Pages.Shared.Components.TiknasPageToolbar.Button;

public class TiknasPageToolbarButtonViewComponent : TiknasViewComponent
{
    protected IStringLocalizerFactory StringLocalizerFactory { get; }

    public TiknasPageToolbarButtonViewComponent(IStringLocalizerFactory stringLocalizerFactory)
    {
        StringLocalizerFactory = stringLocalizerFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync(
        ILocalizableString text,
        string name,
        string icon,
        string id,
        ILocalizableString? busyText,
        FontIconType iconType,
        TiknasButtonType type,
        TiknasButtonSize size,
        bool disabled)
    {
        Check.NotNull(text, nameof(text));

        return View(
            "~/Pages/Shared/Components/TiknasPageToolbar/Button/Default.cshtml",
            new TiknasPageToolbarButtonViewModel(
                await text.LocalizeAsync(StringLocalizerFactory),
                name,
                icon,
                id,
                busyText == null ? null : (await busyText.LocalizeAsync(StringLocalizerFactory)).ToString(),
                iconType,
                type,
                size,
                disabled
            )
        );
    }

    public class TiknasPageToolbarButtonViewModel
    {
        public string Text { get; }
        public string Name { get; }
        public string Icon { get; }
        public string Id { get; }
        public string? BusyText { get; }
        public FontIconType IconType { get; }
        public TiknasButtonType Type { get; }
        public TiknasButtonSize Size { get; }
        public bool Disabled { get; }

        public TiknasPageToolbarButtonViewModel(
            string text,
            string name,
            string icon,
            string id,
            string? busyText,
            FontIconType iconType,
            TiknasButtonType type,
            TiknasButtonSize size,
            bool disabled)
        {
            Text = text;
            Name = name;
            Icon = icon;
            Id = id;
            BusyText = busyText;
            IconType = iconType;
            Type = type;
            Size = size;
            Disabled = disabled;
        }
    }
}
