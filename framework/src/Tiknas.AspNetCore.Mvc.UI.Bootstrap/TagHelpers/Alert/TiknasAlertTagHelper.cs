using Tiknas.AspNetCore.Mvc.UI.Alerts;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Alert;

public class TiknasAlertTagHelper : TiknasTagHelper<TiknasAlertTagHelper, TiknasAlertTagHelperService>
{
    public AlertType AlertType { get; set; } = AlertType.Default;

    public bool? Dismissible { get; set; }

    public TiknasAlertTagHelper(TiknasAlertTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
