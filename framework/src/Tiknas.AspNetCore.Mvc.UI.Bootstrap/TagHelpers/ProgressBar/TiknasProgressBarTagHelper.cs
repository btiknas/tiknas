using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.ProgressBar;

[HtmlTargetElement("tiknas-progress-bar")]
[HtmlTargetElement("tiknas-progress-part")]
public class TiknasProgressBarTagHelper : TiknasTagHelper<TiknasProgressBarTagHelper, TiknasProgressBarTagHelperService>
{
    public double Value { get; set; }

    public double MinValue { get; set; } = 0;

    public double MaxValue { get; set; } = 100;

    public TiknasProgressBarType Type { get; set; } = TiknasProgressBarType.Default;

    public bool? Strip { get; set; }

    public bool? Animation { get; set; }

    public TiknasProgressBarTagHelper(TiknasProgressBarTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
