using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal;

[HtmlTargetElement("tiknas-modal-footer")]
public class TiknasModalFooterTagHelper : TiknasTagHelper<TiknasModalFooterTagHelper, TiknasModalFooterTagHelperService>
{
    public TiknasModalButtons Buttons { get; set; }
    public ButtonsAlign ButtonAlignment { get; set; } = ButtonsAlign.Default;

    public TiknasModalFooterTagHelper(TiknasModalFooterTagHelperService tagHelperService)
        : base(tagHelperService)
    {
    }
}
